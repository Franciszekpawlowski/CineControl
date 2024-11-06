using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetCurrentMovies()
        {
            var currentMovies = await _context.Movies
                .Where(m => m.ReleaseDate <= DateTime.UtcNow) 
                .ToListAsync();
            return Ok(currentMovies);
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetUpcomingMovies()
        {
            var upcomingMovies = await _context.Movies
                .Where(m => m.ReleaseDate > DateTime.UtcNow) 
                .ToListAsync();
            return Ok(upcomingMovies);
        }


        [HttpGet("top-rated")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTopRatedMovies()
        {
            var topRatedMovies = await _context.Movies
                .OrderByDescending(m => m.Rating)
                .Take(10)
                .ToListAsync();
            return Ok(topRatedMovies);
        }

        [HttpGet("recommendations/{userId}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetPersonalizedRecommendations(int userId)
        {
            // Implementacja logiki rekomendacji według potrzeb
            // Na potrzeby przykładu zwróćmy top 5 najlepiej ocenianych filmów
            var recommendedMovies = await _context.Movies
                .OrderByDescending(m => m.Rating)
                .Take(5)
                .ToListAsync();
            return Ok(recommendedMovies);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
