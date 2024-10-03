using CineControl.IdentityService.API.Models.DTO;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestRequestDTO registerRequest)
        {
            var errorMessage = await _authService.RegisterAsync(registerRequest);
            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok(_response);
            }
            _response.IsSuccess = false;
            _response.Message = errorMessage;
            return BadRequest(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResponse = await _authService.LoginAsync(loginRequest);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Login failed";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}