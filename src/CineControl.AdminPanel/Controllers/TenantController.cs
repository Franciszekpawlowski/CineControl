using CineControl.AdminPanel.Models.DTO;
using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.AdminPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.AdminPanel.Controllers;

[Authorize]
public class TenantController(
    ITenantService tenantService,
    ITenantOperatorService tenantOperatorService
) : Controller
{
    public readonly ITenantService _tenantService = tenantService;
    public readonly ITenantOperatorService _tenantOperatorService = tenantOperatorService;
    // GET: TenantController
    public async Task<ActionResult> Index()
    {
        var tenant = await _tenantService.GetTenantsAsync();

        return View(tenant.Value);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateTenantRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _tenantService.CreateTenantAsync(request);
        if(!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Edit(Guid id)
    {
        var tenant = await _tenantService.GetByIdAsync(id);
        if (tenant == null)
        {
            return NotFound();
        }
        return View(tenant.Value.ToUpdate());
    }

    [HttpPost]
    public async Task<ActionResult> Edit(Guid id, UpdateTenantRequest request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _tenantService.UpdateAsync(id,request);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);
        }
        return RedirectToAction("Index");
    }

    public async Task<ActionResult> Details(Guid id)
    {
        var tenant = await _tenantService.GetByIdAsync(id);
        if (!tenant.IsSuccess)
        {
            return NotFound();
        }
        var tenantOperator = await _tenantOperatorService.GetTenantOperatorAsync(id);
        if (!tenantOperator.IsSuccess)
        {
            return NotFound();
        }
        TenantOperatorModelView tenantOperatorModelView = new() {
            Tenant = tenant.Value,
            TenantOperator = tenantOperator.Value
        };
        return View(tenantOperatorModelView);
    }

    public async Task<ActionResult> Delete(Guid id)
    {
        var tenant = await _tenantService.GetByIdAsync(id);
        if (tenant == null)
        {
            return NotFound();
        }
        return View(tenant.Value);
    }

    public async Task<ActionResult> DeleteConfirmed(Guid id)
    {
        var result = await _tenantService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View();
        }
        return RedirectToAction("Index");
    }
}
