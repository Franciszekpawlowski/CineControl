using System.Security.Claims;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.Request.Auth;
using CineControl.IdentityService.API.Models.Response.Auth;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.Auth;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

    

        public async Task<GenericResults<LoginResults>> LoginAsync(LoginRequest loginRequestDTO)
        {
            var result = new GenericResults<LoginResults>();
            ApplicationUser? user = await _userManager.FindByNameAsync(loginRequestDTO.Username);
            if (user is null)
            {
                result.AddError("Invalid username");
                return result;
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password );

            if (!isValid)
            {
                result.AddError("Invalid password");
                return result;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            LoginResults loginResults = new()
            {
                RefreshToken = refreshToken,
                AccessToken = token
            };
            result.SetData(loginResults);
            return result;
        }

        public async Task<GenericResults<RegistationResults>> RegisterAsync(RegisterRequest registerRequest)
        {
            var result = new GenericResults<RegistationResults>();
            ApplicationUser user = new()
            {
                UserName = registerRequest.Username,
                NormalizedUserName = registerRequest.Username.ToUpper(),
                Email = registerRequest.Email,
                NormalizedEmail = registerRequest.Email.ToUpper(),
                EmailConfirmed = true
            };
            try
            {
                var createAsyncResult = await _userManager.CreateAsync(user, registerRequest.Password);
                if (!createAsyncResult.Succeeded)
                {
                    result.AddErrors(createAsyncResult.Errors.Select(error => error.Description));
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);   
            }
            return result;
        }

        public async Task<GenericResults<RefreshTokenResult>> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequestDTO)
        {
            var result = new GenericResults<RefreshTokenResult>();
            ClaimsPrincipal? principal = _jwtTokenGenerator.GetTokenPrincipal(refreshTokenRequestDTO.Token);

            if (principal?.Identity?.Name is null)
            {
                result.AddError("Invalid token");
                return result;
            }

            ApplicationUser? user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user is null || user.RefreshToken != refreshTokenRequestDTO.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                result.AddError("Invalid token");
                return result;
            }

            var RefreshTokenResult = new RefreshTokenResult(){
                AccessToken = _jwtTokenGenerator.GenerateToken(user),
                RefreshToken = _jwtTokenGenerator.GenerateRefreshToken()
            };
            
            result.SetData(RefreshTokenResult);

            user.RefreshToken = RefreshTokenResult.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(2);

            var UpdateAsync = await _userManager.UpdateAsync(user);

            if(!UpdateAsync.Succeeded)
            {
                result.AddErrors(UpdateAsync.Errors.Select(error => error.Description));
            }

            return result;
        }
    }
}