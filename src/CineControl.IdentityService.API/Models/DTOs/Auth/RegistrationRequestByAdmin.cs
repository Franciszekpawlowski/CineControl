namespace CineControl.IdentityService.API.Models.DTOs.Auth;

public class RegistrationRequestByAdmin
{
    public Guid TenantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}