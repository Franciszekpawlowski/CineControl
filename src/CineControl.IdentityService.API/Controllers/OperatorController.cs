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
    public class OperatorController(
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
            var result = await _accountService.LoginAsync(loginRequest,Roles.Operator);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        [Authorize(Policy = nameof(CustomPolicies.Operator))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await _accountService.RegisterAsync(registrationRequest,Roles.Operator);

            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }

        [HttpPost]
        [Authorize(Policy = nameof(CustomPolicies.Admin))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> RegisterByAdmin([FromBody] RegistrationRequestByAdmin registrationRequestByAdmin)
        {
            var result = await _accountService.RegisterByAdminAsync(registrationRequestByAdmin);

            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }

        [HttpPost("{tenantId}")]
        [Authorize(Policy = nameof(CustomPolicies.Admin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetOperators(Guid TenantId)
        {
            var result = await _accountService.GetTenantOperator(TenantId);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost("{tenantId}/{id}")]
        [Authorize(Policy = nameof(CustomPolicies.Admin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetOperatorById(Guid TenantId,string id)
        {
            var result = await _accountService.GetTenantOperatorAsync(TenantId,id);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpDelete("{tenantId}/{id}")]
        [Authorize(Policy = nameof(CustomPolicies.Admin))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteOperator(Guid TenantId,string id)
        {
            var result = await _accountService.DeleteAsync(TenantId,id);

            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

    }
}