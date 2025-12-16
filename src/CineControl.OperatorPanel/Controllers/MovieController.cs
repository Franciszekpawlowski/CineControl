using CineControl.OperatorPanel.Models.DTOs.Movie;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Authorize]
    [Route("[controller]")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _movieService.GetMovieByIdAsync(id);

            return View(model.Value);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _movieService.AddMovieAsync(request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return View(request);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("{id}/Edit")]
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
                // PosterUrl = result.Value.PosterUrl,
                // PanoramicPosterUrl = result.Value.PanoramicPosterUrl,
                Genre = result.Value.Genre,
                Rating = result.Value.Rating
            };
            return View(UpdateMovieRequest);
        }

        [HttpPost("{id}/Edit")]
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
                return View(request);

            }
            return RedirectToAction("Index");
        }

        [HttpGet("{id}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return View(result.Value);
        }

        [HttpPost("{id}/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _movieService.DeleteMovieAsync(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
