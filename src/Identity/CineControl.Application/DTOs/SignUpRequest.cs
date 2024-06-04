using System.Text.Json.Serialization;

namespace CineControll.Application;

public class SignUpRequest
{
    [JsonPropertyName("userEmail")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
