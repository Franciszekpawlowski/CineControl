using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTO;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(
            UserManager<ApplicationUser> userManager
        )
        {
            _userManager = userManager;
        }

    

        public async Task<UserDTO?> GetUser(HttpContext httpContext)
        {
            string? userEmail = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
            ApplicationUser? user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                return null;
            }
            UserDTO userDTO = new() {
                Email = user.Email,
                Username = user.UserName
            };
            return userDTO;
        }

    }
}