using CineControl.Common.Results;
using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs;
using CineControl.SeanceService.API.Models.DTOs.Movies;
using CineControl.SeanceService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class MoviesController : BaseController
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovies()
        {
            var result = await _movieService.GetAllMovies();
            return result.Match(
                onSuccess: movies => Ok(movies.Select(m => m.ToResponse())),
                onFailure: Problem
            );
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovie(int id)
        {
            var result = await _movieService.GetMovieById(id);
            return result.Match(
                onSuccess: movie => Ok(movie.ToResponse()),
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
        {
            var result = await _movieService.AddMovie(request);
            return result.Match(
                onSuccess: movie => CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie.ToResponse()),
                onFailure: Problem
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieResponse updatedMovie)
        {
            var movie = new Movie
            {
                Id = id,
                Title = updatedMovie.Title,
                Description = updatedMovie.Description,
                ShortDescription = updatedMovie.ShortDescription,
                ReleaseDate = updatedMovie.ReleaseDate,
                Duration = updatedMovie.Duration,
                PosterUrl = updatedMovie.PosterUrl,
                PanoramicPosterUrl = updatedMovie.PanoramicPosterUrl,
                Genre = updatedMovie.Genre,
                Rating = updatedMovie.Rating
            };

            var result = await _movieService.UpdateMovie(movie);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovie(id);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        // Dodane endpointy

        [HttpGet("current")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCurrentMovies()
        {
            var result = await _movieService.GetCurrentMovies();
            return result.Match(
                onSuccess: movies => Ok(movies.Select(m => m.ToResponse())),
                onFailure: Problem
            );
        }

        [HttpGet("upcoming")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUpcomingMovies()
        {
            var result = await _movieService.GetUpcomingMovies();
            return result.Match(
                onSuccess: movies => Ok(movies.Select(m => m.ToResponse())),
                onFailure: Problem
            );
        }

        [HttpGet("top-rated")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var result = await _movieService.GetTopRatedMovies();
            return result.Match(
                onSuccess: movies => Ok(movies.Select(m => m.ToResponse())),
                onFailure: Problem
            );
        }

        [HttpGet("recommendations/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPersonalizedRecommendations(int userId)
        {
            var result = await _movieService.GetPersonalizedRecommendations(userId);
            return result.Match(
                onSuccess: movies => Ok(movies.Select(m => m.ToResponse())),
                onFailure: Problem
            );
        }
    }
}
