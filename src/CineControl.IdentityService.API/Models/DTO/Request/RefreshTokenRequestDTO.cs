namespace CineControl.IdentityService.API.Models.DTO.Request
{
    public class RefreshTokenRequestDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}