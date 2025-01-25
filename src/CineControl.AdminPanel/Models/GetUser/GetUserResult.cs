using CineControl.Common.Clients.IdentityService.Models.Account;

namespace CineControl.AdminPanel.Models.GetUser;

public class GetUserResult(GetUserResponseModel getUserResponseModel)
{
    public string UserId { get; set; } = getUserResponseModel.UserId;
    public string Email { get; set; } = getUserResponseModel.Email;
    public string Username { get; set; } = getUserResponseModel.Username;
    public string Role { get; set; } = getUserResponseModel.Role;
}