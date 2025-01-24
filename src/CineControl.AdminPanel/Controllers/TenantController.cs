using System.Threading.Tasks;
using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.AdminPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.AdminPanel.Controllers;

public class TenantController(
    ITenantService tenantService
) : Controller
{
    public readonly ITenantService _tenantService = tenantService;
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
