using System.Text.Json.Serialization;

namespace CineControl.Common.Clients.IdentityService.Models.Login;

public class LoginResponseModel
{
    [JsonPropertyName("accessToken")]
    public required string Token { get; init; } = string.Empty;
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; init; } = string.Empty;
    // public int ExpiresIn { get; init; } = 0;

}