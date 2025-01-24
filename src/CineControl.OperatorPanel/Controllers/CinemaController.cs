using CineControl.OperatorPanel.Models.DTOs;
using CineControl.OperatorPanel.Models.DTOs.Cinemas;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers;

[Authorize]
[Route("/")]
[Route("[controller]")]
public class CinemaController(
    ICinemaService cinemaService,
    ITheaterService theaterService) : Controller
{
    private readonly ICinemaService _cinemaService = cinemaService; 
    private readonly ITheaterService _theaterService = theaterService;

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var model = await _cinemaService.GetCinemasAsync();
        return View(model.Value);
    }

    [HttpGet("Create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AddCinemaRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _cinemaService.AddCinemaAsync(request);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View(request);
        }
        return RedirectToAction("Index");
    }

    [HttpGet("Edit/{id}")]
    public async Task<ActionResult> Edit(int id)
    {
        var result = await _cinemaService.GetCinemasByIdAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        UpdateCinemaRequest updateCinemaRequest = new()
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Address = result.Value.Address,
            City = result.Value.City,
            State = result.Value.State,
            ZipCode = result.Value.ZipCode
        };
        return View(updateCinemaRequest);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, UpdateCinemaRequest request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var result = await _cinemaService.UpdateCinemaAsync(id, request);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    [HttpGet("Details/{id}")]
    [HttpGet("Details")]
    public async Task<ActionResult> Details(int id)
    {
        var cinemaResult = await _cinemaService.GetCinemasByIdAsync(id);
        if (!cinemaResult.IsSuccess)
        {
            ModelState.AddModelError("Error", cinemaResult.Error.ToString());
            return RedirectToAction("Index");
        }
        var theaterResult = await _theaterService.GetTheatersAsync(id);
        if (!theaterResult.IsSuccess)
        {
            ModelState.AddModelError("Error", theaterResult.Error.ToString());
            return RedirectToAction("Index");
        }
        var ViewModel = new CinemaViewModel {
            Cinema = cinemaResult.Value,
            Theaters = theaterResult.Value
        };
        return View(ViewModel);
    }

    [HttpGet("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _cinemaService.GetCinemasByIdAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        return View(result.Value);
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        var result = await _cinemaService.DeleteCinemaAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}
