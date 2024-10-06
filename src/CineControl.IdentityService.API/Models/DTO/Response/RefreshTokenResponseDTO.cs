namespace CineControl.IdentityService.API.Models.DTO.Response
{
    public class RefreshTokenResponseDTO : BaseResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}