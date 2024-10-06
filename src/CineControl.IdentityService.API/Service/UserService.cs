using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authentication;
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
            var token = await httpContext.GetTokenAsync("access_token");
            if (token is null)
            {
                result.AddError("Invalid token");
                return result;
            }
            var claimsPrincipal = _jwtTokenGenerator.GetTokenPrincipal(token);
            if (claimsPrincipal is null)
            {
                result.AddError("Invalid token");
                return result;
            }
            ApplicationUser user = await _userManager.FindByNameAsync(claimsPrincipal.Identity.Name);
            if (user is null)
            {
                result.AddError("Invalid token");
                return result;
            }
            var GetUserResults = new GetUserResults(user);
            result.SetData(GetUserResults);
            return result;
        }

    }
}