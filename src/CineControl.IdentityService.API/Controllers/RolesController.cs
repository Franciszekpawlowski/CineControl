using CineControl.Common.Enums;
using CineControl.IdentityService.API.Models.Request.Roles;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.IdentityService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Policy = nameof(CustomPolicies.Admin))]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPut]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest request)
        {
            var result = await _roleService.AddRoleAsync(request);
            if (!result.IsSuccess)
            {
                return Error(result);
            }
            return Ok();
        }
    }


}