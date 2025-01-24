using CineControl.Common.Enums;
using CineControl.Common.Results;
using CineControl.IdentityService.API.Models.DTOs.Auth;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminController(
        IAccountService accountService
        ) : BaseController
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _accountService.LoginAsync(loginRequest, Roles.Admin);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        [Authorize(Policy = nameof(CustomPolicies.Admin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registerRequest)
        {
            var result = await _accountService.RegisterAsync(registerRequest, Roles.Admin);

            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }
    }
}