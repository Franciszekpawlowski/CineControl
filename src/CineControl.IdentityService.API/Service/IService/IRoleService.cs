using CineControl.IdentityService.API.Models.Request.Roles;
using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.Roles;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IRoleService 
    {
        Task<GenericResults<AddRoleResult>> AddRoleAsync(AddRoleRequest request);
    }
}