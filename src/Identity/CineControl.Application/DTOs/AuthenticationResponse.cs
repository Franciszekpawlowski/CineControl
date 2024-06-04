using System.Text.Json.Serialization;

namespace CineControll.Application;

public class AuthenticationResponse
{
    [JsonPropertyName("succeded")]
    public bool Succeded { get; set;}

    [JsonPropertyName("error")]
    public Dictionary<string, string> Error { get; set;}
}
