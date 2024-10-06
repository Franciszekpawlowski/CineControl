using System.Security.Claims;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTO.Request;
using CineControl.IdentityService.API.Models.DTO.Response;
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

    

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(loginRequestDTO.Username);
            if (user is null)
            {
                return new LoginResponseDTO() {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "User not found",
                };
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password );

            if (!isValid)
            {
                return new LoginResponseDTO() {
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "Invalid password"
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            LoginResponseDTO loginResponse = new()
            {
                RefreshToken = refreshToken,
                AccessToken = token
            };

            return loginResponse;
        }

        public async Task<string> RegisterAsync(RegisterRequestRequestDTO registerRequest)
        {
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
                IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error creating user ${ex.Message}";   
            }
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            ClaimsPrincipal? principal = _jwtTokenGenerator.GetTokenPrincipal(refreshTokenRequestDTO.Token);

            LoginResponseDTO response = new();


            if (principal?.Identity?.Name is null)
            {
                response.Message = "Invalid token";
                return response;
            }

            ApplicationUser? user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user is null || user.RefreshToken != refreshTokenRequestDTO.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                response.Message = "Invalid token";
                return response;
            }
            
            response.AccessToken = _jwtTokenGenerator.GenerateToken(user);
            response.RefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = response.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(2);
            await _userManager.UpdateAsync(user);

            return response;
        }
    }
}