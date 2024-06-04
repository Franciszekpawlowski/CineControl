using System.Text.Json.Serialization;

namespace CineControll.Application;

public class ResetPasswordRequest
{
    [JsonPropertyName("userEmail")]
    public string UserEmail { get; set;}

    [JsonPropertyName("token")]
    public string Token { get; set;}

    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set;}
}
