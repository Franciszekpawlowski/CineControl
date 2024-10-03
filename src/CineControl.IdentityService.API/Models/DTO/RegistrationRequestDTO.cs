namespace CineControl.IdentityService.API.Models.DTO
{
    public class RegisterRequestRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}