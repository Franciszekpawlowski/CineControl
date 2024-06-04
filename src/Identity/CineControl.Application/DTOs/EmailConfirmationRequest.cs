using System.Text.Json.Serialization;

namespace CineControll.Application;

public class EmailConfirmationRequest
{
    [JsonPropertyName("userId")]
    public string UserId { get; set;}

    [JsonPropertyName("token")]
    public string Token { get; set;}
}
