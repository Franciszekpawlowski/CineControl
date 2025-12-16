using CineControl.Common.Clients.IdentityService.Models.Account;

namespace CineControl.OperatorPanel.Models.GetUser
{
    public class GetUserResult
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public GetUserResult(GetUserResponseModel getUserResponseModel)
        {
            UserId = getUserResponseModel.UserId;
            Email = getUserResponseModel.Email;
            Username = getUserResponseModel.Username;
            Role = getUserResponseModel.Role;
        }
    }
}