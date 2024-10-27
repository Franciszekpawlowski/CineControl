namespace CineControl.IdentityService.API.Models.Request.Roles
{
    public class AddRoleRequest
    {
        public string UserName { get; set; }
        public Common.Enums.Roles Role { get; set; }
    }
}