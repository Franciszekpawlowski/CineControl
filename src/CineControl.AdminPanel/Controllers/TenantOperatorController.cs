using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.AdminPanel.Models.DTO.TetantOperator;
using CineControl.AdminPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.AdminPanel.Controllers;

[Authorize]
public class TenantOperatorController(
    ITenantOperatorService tenantOperatorService
) : Controller
{
    public readonly ITenantOperatorService _tenantOperatorService = tenantOperatorService;

    public ActionResult Create(Guid tenantId)
    {
        CreateOperatorRequest request = new() {
            TenantId = tenantId
        };
        return View(request);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateOperatorRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _tenantOperatorService.CreateTenantOperatorAsync(request);
        if(!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);
        }
        return RedirectToAction("Details","Tenant",new { id = request.TenantId });
    }

    // public async Task<ActionResult> Edit(Guid id)
    // {
    //     var tenant = await _tenantService.GetByIdAsync(id);
    //     if (tenant == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(tenant.Value.ToUpdate());
    // }

    // public async Task<ActionResult> Edit(Guid id, UpdateTenantRequest request)
    // {
    //     if (id != request.Id)
    //     {
    //         return NotFound();
    //     }
    //     if (!ModelState.IsValid)
    //     {
    //         return View(request);
    //     }
    //     var result = await _tenantService.UpdateAsync(id,request);
    //     if (!result.IsSuccess)
    //     {
    //         ModelState.AddModelError("Error", result.Error.ToString());
    //         return View(request);
    //     }
    //     return RedirectToAction("Index");
    // }

    public async Task<ActionResult> Delete(Guid tenantId, Guid id)
    {
        var req = await _tenantOperatorService.GetTenantOperatorByIdAsync(tenantId,id);
        if (!req.IsSuccess)
        {
            return NotFound();
        }
        var tenantOperator = req.Value;
        tenantOperator.TenantId = tenantId;
        return View(tenantOperator);
    }

    public async Task<ActionResult> DeleteConfirmed(Guid tenantId, Guid id)
    {
        var result = await _tenantOperatorService.DeleteTenantOperatorAsync(tenantId,id);
        if (!result.IsSuccess)
        {

            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Delete");
        }
        return RedirectToAction("Details","Tenant",new { id = tenantId });
    }
}
