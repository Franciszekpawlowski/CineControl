namespace CineControl.IdentityService.API.Models.DTO
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UserDTO? User { get; set; } 
    }
}