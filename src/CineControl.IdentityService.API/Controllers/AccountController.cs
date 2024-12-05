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
    public class AccountController(
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
            var result = await _accountService.LoginAsync(loginRequest);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _accountService.RegisterAsync(registerRequest);

            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var result = await _accountService.RefreshTokenAsync(refreshTokenRequest);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}