using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Services;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeancesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFilmService _filmService;

        public SeancesController(AppDbContext context, IFilmService filmService)
        {
            _context = context;
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
        {
            return await _context.Seances.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seance>> GetSeance(int id)
        {
            var seance = await _context.Seances.FindAsync(id);

            if (seance == null)
            {
                return NotFound();
            }

            return seance;
        }

        [HttpPost]
        public async Task<ActionResult<Seance>> PostSeance(Seance seance)
        {
            var film = await _filmService.GetFilmById(seance.FilmId);
            if (film == null)
            {
                return BadRequest("Film not found.");
            }

            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeance), new { id = seance.Id }, seance);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeance(int id, Seance seance)
        {
            if (id != seance.Id)
            {
                return BadRequest();
            }

            var film = await _filmService.GetFilmById(seance.FilmId);
            if (film == null)
            {
                return BadRequest("Film not found.");
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
