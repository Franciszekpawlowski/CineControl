using CineControl.IdentityService.API.Models.Request.Auth;
using CineControl.IdentityService.API.Models.Response;
using CineControl.IdentityService.API.Models.Response.Auth;
using CineControl.IdentityService.API.Models.Results.Auth;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);
            if (!result.IsSuccess)
            {
                return Error(result);
            }
            LoginResponse loginResponse = new(){
                AccessToken = result.Data.AccessToken,
                RefreshToken = result.Data.RefreshToken
            };
            return Ok(loginResponse);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authService.RegisterAsync(registerRequest);
            if (!result.IsSuccess)
            {
                return Error(result);
            }

            return Ok(result.Data.Message);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequestDTO)
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenRequestDTO);
            if (!result.IsSuccess)
            {
                return Error(result);
            }
            RefreshTokenResponse RefreshTokenResponse = new(){
                AccessToken = result.Data.AccessToken,
                RefreshToken = result.Data.RefreshToken
            };
            return Ok(RefreshTokenResponse);
        }
    }
}