namespace CineControl.Common.Clients.IClients;

public interface IAuthServiceClient {
    Task<string> LoginAsync(); 
}