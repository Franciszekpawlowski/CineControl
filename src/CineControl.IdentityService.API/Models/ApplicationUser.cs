using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ChainId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}