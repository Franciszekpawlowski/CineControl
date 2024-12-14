using System.Text.Json.Serialization;

namespace CineControl.Common.Clients.IdentityService.Models.Login;

public class LoginResponseModel
{
    [JsonPropertyName("AccessToken")]
    public required string Token { get; init; } = string.Empty;
    [JsonPropertyName("RefreshToken")]
    public required string RefreshToken { get; init; } = string.Empty;
    // public int ExpiresIn { get; init; } = 0;

}