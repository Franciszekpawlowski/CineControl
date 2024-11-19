using CineControl.Common.Clients.Authenticator;
using CineControl.Common.Clients.IClients;
using RestSharp;

namespace CineControl.Common.Clients;

public class AuthServiceClient : IAuthServiceClient, IDisposable
{
    readonly RestClient _client;
    public AuthServiceClient(string tmp,string tmp2,string tmp3)
    {
        var options = new RestClientOptions("http://localhost:5000/api"){
            Authenticator = new InternalAuthenticator(tmp,tmp2,tmp3)
        };
        _client = new RestClient(options);
    }
    record AuthServiceSingleObject<T>(T Data);
    public async Task<string> LoginAsync()
    {
        var response = await _client.GetAsync<AuthServiceSingleObject<string>>("/auth/login");
        return response!.Data;
    }
    public void Dispose()
    {
        _client?.Dispose();
        GC.SuppressFinalize(this);
    }
}