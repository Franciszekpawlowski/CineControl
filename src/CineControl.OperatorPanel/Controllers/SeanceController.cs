using CineControl.OperatorPanel.Models.DTOs.Seances;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers;

public class SeanceController(
    ISeanceService seanceService
) : Controller
{
    private readonly ISeanceService _seanceService = seanceService;
    public async Task<IActionResult> Index(
        
    )
    {
        var model = await _seanceService.GetSeancesAsync();

        return View(model.Value);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AddSeanceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _seanceService.AddSeanceAsync(request);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _seanceService.GetSeancesByIdAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        var UpdateMovieRequest = new UpdateSeanceRequest() {
            Id = result.Value.Id,
            MovieId = result.Value.MovieId,
            MovieTitle = result.Value.MovieTitle,
            TheaterId = result.Value.TheaterId,
            CinemaId = result.Value.CinemaId,
            PosterUrl = result.Value.PosterUrl,
            StartTime = result.Value.StartTime,
            EndTime = result.Value.EndTime
        };
        return View(UpdateMovieRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, UpdateSeanceRequest request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _seanceService.UpdateSeanceAsync(id, request);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);

        }
        return RedirectToAction("Index");
    }

    public async Task<ActionResult> Delete(int id)
    {
        var result = await _seanceService.GetSeancesByIdAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        return View(result.Value);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        var result = await _seanceService.DeleteSeanceAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View();
        }
        return RedirectToAction("Index");
    }
}
