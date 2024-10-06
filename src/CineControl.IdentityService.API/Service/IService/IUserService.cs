using CineControl.IdentityService.API.Models.DTO;

namespace CineControl.IdentityService.API.Service.IService
{
    public interface IUserService
    {
        public Task<UserDTO?> GetUser(HttpContext httpContext);
    }
}