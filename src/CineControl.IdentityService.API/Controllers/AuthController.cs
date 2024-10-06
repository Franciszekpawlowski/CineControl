using CineControl.IdentityService.API.Models.DTO.Request;
using CineControl.IdentityService.API.Models.DTO.Response;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected BaseResponseDTO _response;
        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestRequestDTO registerRequest)
        {
            var errorMessage = await _authService.RegisterAsync(registerRequest);
            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok(_response);
            }
            _response.Message = errorMessage;
            return BadRequest(_response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResponse = await _authService.LoginAsync(loginRequest);
            if (loginResponse.AccessToken == null)
            {
                _response.Message = loginResponse.Message;
                return BadRequest(_response);
            }
            LoginResponseDTO loginResponseDTO = new(){
                AccessToken = loginResponse.AccessToken,
                RefreshToken = loginResponse.RefreshToken
            };
            return Ok(loginResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            var loginresult = await _authService.RefreshTokenAsync(refreshTokenRequestDTO);
            if (loginresult.AccessToken == null)
            {
                _response.Message = loginresult.Message;
                return BadRequest(_response);
            }
            LoginResponseDTO loginResponseDTO = new(){
                AccessToken = loginresult.AccessToken,
                RefreshToken = loginresult.RefreshToken
            };
            return Ok(loginResponseDTO);
        }
    }
}