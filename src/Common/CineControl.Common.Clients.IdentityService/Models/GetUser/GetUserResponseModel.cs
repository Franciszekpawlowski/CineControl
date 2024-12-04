namespace CineControl.Common.Clients.AuthService.Models.GetUser;

public class GetUserResponseModel
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}