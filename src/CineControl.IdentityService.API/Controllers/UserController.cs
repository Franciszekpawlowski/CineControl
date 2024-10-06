using CineControl.IdentityService.API.Models.DTO;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService
        )
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            UserDTO? userDTO = await _userService.GetUser(HttpContext);
            if (userDTO is null)
            {
                return Unauthorized();
            }
            return Ok(userDTO);
        }
    }
}