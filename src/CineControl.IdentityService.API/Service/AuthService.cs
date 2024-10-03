using System.Diagnostics;
using CineControl.IdentityService.API.Data;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTO;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CineControl.IdentityService.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly appdbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            appdbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

    

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            ApplicationUser? user = await _context.applicationUsers.FirstOrDefaultAsync(u => u.Username.ToLower() == loginRequestDTO.Username.ToLower());

            if (user is null)
            {
                return new LoginResponseDTO() {
                    AccessToken = "",
                    RefreshToken = "",
                    User = null
                };
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (isValid)
            {
                return new LoginResponseDTO() {
                    AccessToken = "",
                    RefreshToken = "",
                    User = null
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDTO userDTO = new()
            {
                Email = user.Email,
                Id = user.Id,
                Username = user.Username
            };

            LoginResponseDTO loginResponse = new()
            {
                User = userDTO,
                AccessToken = token
            };

            return loginResponse;
        }

        public async Task<string> RegisterAsync(RegisterRequestRequestDTO registerRequest)
        {
            ApplicationUser user = new()
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                NormalizedEmail = registerRequest.Email.ToUpper(),
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await _userManager.FindByEmailAsync(registerRequest.Email);
                    UserDTO userDTO = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Username = userToReturn.UserName
                    };
                    return "";
                }
                else
                {
                    var tmp = result.Errors.FirstOrDefault().Description;
                    return tmp;
                }
            }
            catch (Exception ex)
            {
                return $"Error creating user ${ex.Message}";   
            }
            
        }
    }
}