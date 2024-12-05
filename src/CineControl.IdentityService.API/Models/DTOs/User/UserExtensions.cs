using System.Security.Claims;
using CineControl.Common.Enums;

namespace CineControl.IdentityService.API.Models.DTOs.User;

public static class UserExtensions
{
    public static GetUserResponse ToResponse(
        this ApplicationUser applicationUser, Roles role
    ) => new()
    {
        UserId = applicationUser.Id,
        Username = applicationUser.UserName,
        Email = applicationUser.Email,
        Role = role.ToString(),
    };
}
