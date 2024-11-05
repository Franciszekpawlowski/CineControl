using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserService(
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator
        )
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }



        public async Task<GenericResults<GetUserResults>> GetUser(HttpContext httpContext)
        {
            var result = new GenericResults<GetUserResults>();
            var identity = httpContext.User;
            if (identity == null)
            {
                result.AddError("Invalid token");
                return result;
            }
            var username = identity.FindFirstValue(JwtRegisteredClaimNames.Name);
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            if (user is null)
            {
                result.AddError("Invalid token");
                return result;
            }
            List<Claim> claims = new(await _userManager.GetClaimsAsync(user));
            var role = claims.FirstOrDefault(c => c.Type == CustomClaims.Role);
            var test = Enum.TryParse(role?.Value, out Roles roleEnum);
            var getUserResults = new GetUserResults(user, roleEnum);
            result.SetData(getUserResults);
            return result;
        }

    }
}