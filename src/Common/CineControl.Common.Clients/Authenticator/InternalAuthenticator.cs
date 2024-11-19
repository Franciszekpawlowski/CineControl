using System.Data;
using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.Authenticator;

public class InternalAuthenticator(string baseUrl, string clientId, string clientSecret) : AuthenticatorBase("")
{
    readonly string _baseUrl = baseUrl;
    readonly string _clientId = clientId;
    readonly string _clientSecret = clientSecret;

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrEmpty(accessToken) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }
    class data {
        public string username;
        public string password;
    }
    record TokenResponse(string Access_token, string Token_type);
    private async Task<string> GetToken()
    {
        var param = new data {
            username = _clientId,
            password = _clientSecret
        };
        var options = new RestClientOptions(_baseUrl);
        using var client = new RestClient(options);
        var request = new RestRequest("").AddJsonBody(param);
        var response = await client.PostAsync<TokenResponse>(request);
        return $"Bearer {response!.Access_token}";
    }
}