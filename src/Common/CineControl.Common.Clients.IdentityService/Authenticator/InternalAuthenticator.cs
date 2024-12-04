using RestSharp;
using RestSharp.Authenticators;

namespace CineControl.Common.Clients.AuthService.Authenticator;

public class InternalAuthenticator(
    string baseUrl, string accessToken,
    string refreshToken,
    DateTime refrehTokenExpiration) : AuthenticatorBase("")
{
    readonly string _baseUrl = baseUrl;
    readonly string _accessToken = accessToken;
    protected string RefreshToken { get; private set; } = refreshToken;
    protected DateTime RefrehTokenExpiration { get; private set; } = refrehTokenExpiration;

    protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
    {
        if (DateTime.Now >= RefrehTokenExpiration)
        {
            var tokenResponse = await RefreshTokenAsync();
            Token = tokenResponse.Token;
            RefrehTokenExpiration = tokenResponse.GetExpirationDate(DateTime.Now);
            RefreshToken = tokenResponse.RefreshToken;
        }
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }
    private async Task<TokenResponse> RefreshTokenAsync()
    {
        var options = new RestClientOptions(_baseUrl);
        using var client = new RestClient(options);
        var request = new RestRequest("/auth/refresh");
        var body = new TokenRequest
        {
            Token = _accessToken,
            RefreshToken = RefreshToken
        };
        request.AddJsonBody(body);
        return await client.PostAsync<TokenResponse>(request);
    }

    record TokenRequest
    {
        public required string Token { get; init; }
        public required string RefreshToken { get; init; }
    }

    record TokenResponse
    {
        public required string Token { get; init; }
        public required string RefreshToken { get; init; }
        public int ExpiresIn { get; init; }

        public DateTime GetExpirationDate(DateTime baseDateTime, int toleranceInMinutes = 1) =>
            baseDateTime.AddSeconds(ExpiresIn).AddMinutes(-toleranceInMinutes);
    }
}