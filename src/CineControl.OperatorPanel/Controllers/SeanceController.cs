using System.Threading.Tasks;
using CineControl.OperatorPanel.Models.DTOs.Seances;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineControl.OperatorPanel.Controllers;

[Route("Cinema/{cinemaId}/[controller]")]
public class SeanceController(
    ISeanceService seanceService,
    IMovieService movieServcie,
    ITheaterService theaterService
) : Controller
{
    private readonly ISeanceService _seanceService = seanceService;
    private readonly IMovieService _movieService = movieServcie;
    private readonly ITheaterService _theaterService = theaterService;
    [HttpGet("Index")]
    public async Task<IActionResult> Index(
        [FromRoute]int cinemaId
    )
    {
        var model = await _seanceService.GetSeancesByCinemaIdAsync(cinemaId);
        ViewBag.cinemaId = cinemaId;
        return View(model.Value);
    }

    [HttpGet("Create")]
    public async Task<ActionResult> Create(
        [FromRoute]int cinemaId
    )
    {
        var selectListMovies = await _movieService.GetMoviesAsync();
        var selectListTheaters = await _theaterService.GetTheatersAsync(cinemaId);
        AddSeanceRequest req = new() {
            Movies = selectListMovies.Value.Select(
                x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).ToList(),
            CinemaId = cinemaId,
            Theaters = selectListTheaters.Value.Select(
                x => new SelectListItem
                {
                    Text = x.Name, 
                    Value = x.Id.ToString()
                }).ToList()
        };
        return View(req);
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AddSeanceRequest request)
    {
        if (!ModelState.IsValid)
        {
            var selectListMovies = await _movieService.GetMoviesAsync();
            var selectListTheaters = await _theaterService.GetTheatersAsync(request.CinemaId);
            request.Movies = selectListMovies.Value.Select(
                    x => new SelectListItem
                    {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    }).ToList();
            request.Theaters = selectListTheaters.Value.Select(
                    x => new SelectListItem
                    {
                        Text = x.Name, 
                        Value = x.Id.ToString()
                    }).ToList();
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

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await _seanceService.GetSeanceByIdAsync(id);
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

    [HttpPost("Edit/{id}")]
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

    [HttpGet("Delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _seanceService.GetSeanceByIdAsync(id);
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
        var result = await _seanceService.DeleteSeanceAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", result.Error.ToString());
            return View();
        }
        return RedirectToAction("Index");
    }
}
