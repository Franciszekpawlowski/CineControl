using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using Microsoft.AspNetCore.Identity;

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
