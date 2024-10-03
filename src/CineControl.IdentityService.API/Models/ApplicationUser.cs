using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; } = "";
    }
}