using CineControl.Common.Results;
using CineControl.IdentityService.API.Models.DTOs.User;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IUserService
    {
        public Task<ResultT<GetUserResponse>> GetUser(HttpContext httpContext);
    }
}