using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.IdentityService.API.Errors;
using CineControl.IdentityService.API.Extensions;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTOs.Auth;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class AccountService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        ITenantProvider tenantProvider,
        ITenantServiceClient tenantServiceClient
        ) : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly ITenantServiceClient _tenantServiceClient = tenantServiceClient;
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public async Task<ResultT<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
            {
                return AuthErrors.AccessUnauthorized();
            }
            var claimsList = await _userManager.GetClaimsAsync(user);
            var role = claimsList.FirstOrDefault(claim => claim.Type == CustomClaims.Role);

            if (user.TenantId != _tenantProvider.GetTenantId() && role?.Value == Roles.User.ToString())
            {
                return AuthErrors.AccessUnauthorized();
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!isValid)
            {
                return AuthErrors.AccessUnauthorized();
            }

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            return new LoginResponse() {
                RefreshToken = refreshToken,
                AccessToken = token
            };
        }

        public async Task<Result> RegisterAsync(RegisterRequest registerRequest)
        {
            //todo validate tenantId
            var tenantIdExist = await _tenantServiceClient.GetAsync(_tenantProvider.GetTenantId());
            if (!tenantIdExist.IsSuccess)
            {
                return AuthErrors.NotFound();
            }
            var user = registerRequest.ToApplicationUser();
            user.TenantId = tenantIdExist.Value.Id;
            var createAsyncResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!createAsyncResult.Succeeded)
            {
                return AuthErrors.Conflict();
            }

            var addClaimResult = await AddClaimsAsync(user, registerRequest.Roles);
            if (!addClaimResult.Succeeded)
            {
                return addClaimResult.MapToCustomErrors();
            }
            return Result.Success();
        }

        public async Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            ClaimsPrincipal? principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);

            if (principal is null)
            {
                return AuthErrors.AccessUnauthorized();
            }

            ApplicationUser? user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user is null || user.RefreshToken != refreshTokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return AuthErrors.AccessUnauthorized();
            }

            var AccessToken = await _jwtTokenGenerator.GenerateTokenAsync(user);
            var RefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            var RefreshTokenExpiryTime = DateTime.Now.AddHours(2);

            user.RefreshToken = RefreshToken;
            user.RefreshTokenExpiryTime = RefreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);

            return new RefreshTokenResponse()
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                ExpiresIn = (int)DateTime.Now.Subtract(RefreshTokenExpiryTime).TotalSeconds
            };
        }

        private async Task<IdentityResult> AddClaimsAsync(ApplicationUser user, Roles roles)
        {
            var claims = new List<Claim>
            {
                // new Claim(CustomClaims.Role, Roles.User.ToString()),
                new Claim(CustomClaims.Role, roles.ToString()),
                new Claim(CustomClaims.UserId, user.Id.ToString()),
                new Claim(CustomClaims.TenantId, user.TenantId.ToString())
            };
            return await _userManager.AddClaimsAsync(user, claims);
        }
    }
}