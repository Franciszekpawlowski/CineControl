using System;
using CineControl.TenantService.API.Models;
using CineControl.TenantService.API.Service.IService;
using CineControl.Common.Results;
using CineControl.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.TenantService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Admin))]
    public class TenantController : BaseController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tenantService.GetAllAsync();
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _tenantService.GetByIdAsync(id);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tenant tenant)
        {
            var result = await _tenantService.CreateAsync(tenant);
            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Tenant tenant)
        {
            tenant.Id = id;
            var result = await _tenantService.UpdateAsync(tenant);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tenantService.DeleteAsync(id);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }
    }
}
