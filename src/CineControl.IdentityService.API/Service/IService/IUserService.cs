using CineControl.IdentityService.API.Models.Results;
using CineControl.IdentityService.API.Models.Results.User;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IUserService
    {
        public Task<GenericResults<GetUserResults>> GetUser(HttpContext httpContext);
    }
}