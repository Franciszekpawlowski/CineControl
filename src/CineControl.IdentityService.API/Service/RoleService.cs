using System.Security.Claims;
using CineControl.Common;
using CineControl.Common.Enums;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Models.Request.Roles;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.Roles;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CineControl.IdentityService.API.Service
{
    public class RoleService : IRoleService
    {
        public UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GenericResults<AddRoleResult>> AddRoleAsync(AddRoleRequest request)
        {
            var result = new GenericResults<AddRoleResult>();
            ApplicationUser? user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                return result.AddError("Invalid username");
            }
            var AllClaims = await _userManager.GetClaimsAsync(user);
            var claim = AllClaims.FirstOrDefault(claim => claim.Type == CustomClaims.Role);
            if (claim is not null)
            {
                var removeClaimResult = await _userManager.RemoveClaimAsync(user, claim);
                if (!removeClaimResult.Succeeded)
                {
                    return result.AddErrors(removeClaimResult);
                }
            }
            var addClaimResult = await _userManager.AddClaimAsync(user, new Claim(CustomClaims.Role, request.Role.ToString()));
            if (!addClaimResult.Succeeded)
            {
                return result.AddErrors(addClaimResult);
            }
            return result;
        }
    }
}