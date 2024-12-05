using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.IdentityService.API.Errors;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.DTOs.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class UserService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator
        ) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<ResultT<GetUserResponse>> GetUser(HttpContext httpContext)
        {
            var identity = httpContext.User;
            if (identity == null)
            {
                return AuthErrors.AccessUnauthorized();
            }
            ApplicationUser? user = await _userManager.FindByNameAsync(identity.Identity.Name);
            if (user is null)
            {
                return AuthErrors.NotFound();
            }
            List<Claim> claims = [.. await _userManager.GetClaimsAsync(user)];
            var role = claims.FirstOrDefault(c => c.Type == CustomClaims.Role);
            if (!Enum.TryParse(role?.Value, out Roles roleEnum))
            {
                return AuthErrors.UnprocessableEntity();
            }
            return user.ToResponse(roleEnum);
        }

    }
}