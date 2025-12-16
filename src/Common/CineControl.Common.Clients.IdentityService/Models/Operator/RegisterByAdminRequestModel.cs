namespace CineControl.Common.Clients.IdentityService.Models.Operator;

public class RegisterByAdminRequestModel
{
    public Guid TenantId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

}
