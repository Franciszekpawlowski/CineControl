using CineControl.Common.Results;
using CineControl.IdentityService.API.Models.DTOs.User;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController(
        IUserService userService
        ) : BaseController
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userService.GetUser(HttpContext);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}