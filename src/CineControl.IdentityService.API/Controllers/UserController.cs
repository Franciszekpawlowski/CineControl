using CineControl.IdentityService.API.Models.Response;
using CineControl.IdentityService.API.Models.Response.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(
            IUserService userService
        )
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userService.GetUser(HttpContext);
            if (!result.IsSuccess)
            {
                return Error(result);
            }
            var GetUserResponse = new GetUserResponse()
            {
                UserId = result.Data.User.Id,
                Email = result.Data.User.Email,
                Username = result.Data.User.UserName,
                Role = result.Data.Role.ToString()
            };
            return Ok(GetUserResponse);
        }
    }
}