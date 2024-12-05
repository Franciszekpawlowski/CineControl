using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.IdentityService.API.Errors;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTOs.Auth;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class AccountService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenGenerator jwtTokenGenerator
        ) : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<ResultT<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user is null)
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

            var user = registerRequest.ToApplicationUser();
            var createAsyncResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!createAsyncResult.Succeeded)
            {
                return AuthErrors.Conflict();
            }
            var claim = new Claim(CustomClaims.Role.ToString(), Roles.User.ToString());
            var addClaimResult = await _userManager.AddClaimAsync(user, claim);

            if (!addClaimResult.Succeeded)
            {
                return AuthErrors.UnprocessableEntity();
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
    }
}