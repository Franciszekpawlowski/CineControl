using System.Security.Claims;
using System.Threading.Tasks;
using CineControl.Common;
using CineControl.Common.Clients.TenantService.IClients;
using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.IdentityService.API.Data;
using CineControl.IdentityService.API.Errors;
using CineControl.IdentityService.API.Extensions;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTOs.Auth;
using CineControl.IdentityService.API.Models.DTOs.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CineControl.IdentityService.API.Service
{
    public class AccountService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        ITenantProvider tenantProvider,
        ITenantServiceClient tenantServiceClient,
        appdbContext dbContext,
        ILogger<AccountService> logger
        ) : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly ITenantServiceClient _tenantServiceClient = tenantServiceClient;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly appdbContext _appdbContext = dbContext;
        private readonly ILogger<AccountService> _logger = logger;

        public async Task<ResultT<LoginResponse>> LoginAsync(LoginRequest loginRequest, Roles LoginRole = Roles.User)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user == null)
            {
                _logger.LogError($"User {loginRequest.Username} not found");
                return AuthErrors.AccessUnauthorized();
            }
            var claimsList = await _userManager.GetClaimsAsync(user);
            var role = claimsList.FirstOrDefault(claim => claim.Type == CustomClaims.Role);

            if (role!.Value != LoginRole.ToString())
            {
                _logger.LogError($"User {loginRequest.Username} is not a {LoginRole}");
                return AuthErrors.AccessUnauthorized();
            }

            if (user.TenantId != _tenantProvider.GetTenantId())
            {
                _logger.LogError($"User {loginRequest.Username} is not in the current tenant");
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

        public async Task<Result> RegisterAsync(RegistrationRequest registerRequest, Roles RegisterRole = Roles.User)
        {
            var tenantIdExist = await _tenantServiceClient.GetAsync(_tenantProvider.GetTenantId());
            if (!tenantIdExist.IsSuccess || tenantIdExist.Value.Id == Guid.Empty)
            {
                _logger.LogError(tenantIdExist.Error.Description);
                return AuthErrors.NotFound();
            }
            var user = registerRequest.ToApplicationUser();
            user.TenantId = tenantIdExist.Value.Id;
            var createAsyncResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!createAsyncResult.Succeeded)
            {
                _logger.LogError(createAsyncResult.Errors.Select(e => e.Description)
                    .FirstOrDefault());
                return AuthErrors.Conflict();
            }

            var addClaimResult = await AddClaimsAsync(user, RegisterRole);
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

        public async Task<Result> RegisterByAdminAsync(RegistrationRequestByAdmin registerRequest)
        {
            var tenantIdExist = await _tenantServiceClient.GetAsync(registerRequest.TenantId);
            if (!tenantIdExist.IsSuccess && tenantIdExist.Value.Id == Guid.Empty)
            {
                return AuthErrors.NotFound();
            }
            var user = registerRequest.ToApplicationUser();
            var createAsyncResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!createAsyncResult.Succeeded)
            {
                return AuthErrors.Conflict();
            }

            var addClaimResult = await AddClaimsAsync(user, Roles.Operator);
            if (!addClaimResult.Succeeded)
            {
                return addClaimResult.MapToCustomErrors();
            }
            return Result.Success();
        }

        public async Task<ResultT<IEnumerable<GetUserResponse>>> GetTenantOperator(Guid id)
        {
            var users = (await _userManager.GetUsersForClaimAsync(
                new Claim(CustomClaims.Role, Roles.Operator.ToString())
                ))
                .Where(u => u.TenantId == id)
                .ToList();
            

            var applicationUser = new List<GetUserResponse>();

            if (users == null || !users.Any())
            {
                return applicationUser;
            }


            foreach (var u in users)
            {
                var claims = await _userManager.GetClaimsAsync(u);
                var roleClaim = claims.FirstOrDefault(c => c.Type == CustomClaims.Role)?.Value;

                applicationUser.Add(new GetUserResponse
                {
                    UserId = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Role = roleClaim
                });
            }

            return applicationUser;
        }

        public async Task<ResultT<GetUserResponse>> GetTenantOperatorAsync(Guid tenantId, string id)
        {
            var user = _appdbContext.applicationUsers.Where(u => u.TenantId == tenantId && u.Id == id).FirstOrDefault();
            if (user is null)
            {
                return AuthErrors.NotFound();
            }
            List<Claim> claims = [.. await _userManager.GetClaimsAsync(user)];
            var role = claims.FirstOrDefault(c => c.Type == CustomClaims.Role);
            if (!Enum.TryParse(role?.Value, out Roles roleEnum))
            {
                return AuthErrors.UnprocessableEntity();
            }
            return user.ToResponse(roleEnum);
        }

        public async Task<Result> DeleteAsync(Guid tenantId, string id)
        {
            var user = _appdbContext.applicationUsers.Where(u => u.TenantId == tenantId && u.Id == id).FirstOrDefault();
            if (user is null)
            {
                return AuthErrors.NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return AuthErrors.Conflict();
            }
            return Result.Success();
        }
    }
}