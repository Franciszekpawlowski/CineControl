namespace CineControl.Common.Clients.IdentityService.Models.Account;

public class LoginRequestModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}