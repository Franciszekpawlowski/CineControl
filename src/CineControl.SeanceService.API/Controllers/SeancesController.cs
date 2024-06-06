using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeancesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeancesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeanceDto>>> GetSeances()
        {
            return await _context.Seances
                .Include(s => s.Movie) // Include the related Movie entity
                .Select(s => new SeanceDto
                {
                    Id = s.Id,
                    MovieId = s.MovieId,
                    MovieTitle = s.Movie.Title,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeanceDto>> GetSeance(int id)
        {
            var seance = await _context.Seances
                .Include(s => s.Movie) // Include the related Movie entity
                .Select(s => new SeanceDto
                {
                    Id = s.Id,
                    MovieId = s.MovieId,
                    MovieTitle = s.Movie.Title,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }

        [HttpPost]
        public async Task<ActionResult<SeanceDto>> PostSeance(Seance seance)
        {
            var movie = await _context.Movies.FindAsync(seance.MovieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            seance.EndTime = seance.StartTime.AddMinutes(movie.Duration);
            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();

            var seanceDto = new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = movie.Title,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            };

            return CreatedAtAction(nameof(GetSeance), new { id = seance.Id }, seanceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance(int id, Seance seance)
        {
            if (id != seance.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.FindAsync(seance.MovieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            _context.Entry(seance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
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
        public async Task<IActionResult> DeleteSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return NotFound();
            }

            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeanceExists(int id)
        {
            return _context.Seances.Any(e => e.Id == id);
        }
    }
}
