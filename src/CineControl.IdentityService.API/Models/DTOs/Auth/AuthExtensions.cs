namespace CineControl.IdentityService.API.Models.DTOs.Auth;

public static class AuthExtensions
{
    public static ApplicationUser ToApplicationUser(
        this RegistrationRequest registerRequest
        ) => new()
        {
            UserName = registerRequest.Username,
            NormalizedUserName = registerRequest.Username.ToUpper(),
            Email = registerRequest.Email,
            NormalizedEmail = registerRequest.Email.ToUpper(),
            EmailConfirmed = true
        };

    public static ApplicationUser ToApplicationUser(
        this RegistrationRequestByAdmin registerRequest
        ) => new()
        {
            TenantId = registerRequest.TenantId,
            UserName = registerRequest.Username,
            NormalizedUserName = registerRequest.Username.ToUpper(),
            Email = registerRequest.Email,
            NormalizedEmail = registerRequest.Email.ToUpper(),
            EmailConfirmed = true
        };
}
