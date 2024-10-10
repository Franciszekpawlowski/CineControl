namespace CineControl.IdentityService.API.Models.Results.Auth
{
    public class LoginResults
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
