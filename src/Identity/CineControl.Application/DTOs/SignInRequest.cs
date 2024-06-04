using System.Text.Json.Serialization;

namespace CineControll.Application;

public class SignInRequest
{
    [JsonPropertyName("userEmail")]
    public string Email { get; set;}

    [JsonPropertyName("password")]
    public string Password { get; set;}

    [JsonPropertyName("rememberMe")]
    public bool RememberMe { get; set;}
}
