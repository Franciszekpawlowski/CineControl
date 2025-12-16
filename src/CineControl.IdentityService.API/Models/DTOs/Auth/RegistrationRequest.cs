namespace CineControl.IdentityService.API.Models.DTOs.Auth
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}