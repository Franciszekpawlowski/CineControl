using CineControl.OperatorPanel.Models.DTOs.Movie;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Authorize]
    public class MovieController(
        IMovieService movieServcie
    ) : Controller
    {
        private readonly IMovieService _movieService = movieServcie;

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await _movieService.GetMoviesAsync();

            return View(model.Value);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await _movieService.GetMovieByIdAsync(id);

            return View(model.Value);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _movieService.AddMovieAsync(request);
            return View(result);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            var UpdateMovieRequest = new UpdateMovieRequest() {
                Id = result.Value.Id,
                Title = result.Value.Title,
                Description = result.Value.Description,
                ShortDescription = result.Value.ShortDescription,
                ReleaseDate = result.Value.ReleaseDate,
                Duration = result.Value.Duration,
                PosterUrl = result.Value.PosterUrl,
                PanoramicPosterUrl = result.Value.PanoramicPosterUrl,
                Genre = result.Value.Genre,
                Rating = result.Value.Rating
            };
            return View(UpdateMovieRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateMovieRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _movieService.UpdateMovieAsync(id, request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
            }
            return RedirectToAction("Index");
        }
    }
}
