using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SeancesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeancesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SeanceDto>>> GetSeances()
        {
            var seances = await _context.Seances
                .Include(s => s.Movie)
                .ToListAsync();

            var seanceDtos = seances.Select(seance => new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = seance.Movie.Title,
                TheaterId = seance.TheaterId,
                CinemaId = seance.CinemaId,
                PosterUrl = seance.Movie.PosterUrl,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            }).ToList();

            return seanceDtos;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<SeanceDto>> GetSeance(int id)
        {
            var seance = await _context.Seances
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seance == null)
            {
                return NotFound();
            }

            var seanceDto = new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = seance.Movie.Title,
                TheaterId = seance.TheaterId,
                CinemaId = seance.CinemaId,
                PosterUrl = seance.Movie.PosterUrl,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            };

            return seanceDto;
        }

        [HttpGet("bycinema/{cinemaId}/date/{date}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<SeanceDto>>> GetSeancesByCinemaAndDate(int cinemaId, DateTime date)
        {
            var utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            var seances = await _context.Seances
                .Include(s => s.Movie)
                .Where(s => s.CinemaId == cinemaId && s.StartTime.Date == utcDate.Date)
                .ToListAsync();

            var seanceDtos = seances.Select(seance => new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = seance.Movie.Title,
                TheaterId = seance.TheaterId,
                CinemaId = seance.CinemaId,
                PosterUrl = seance.Movie.PosterUrl,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            }).ToList();

            return Ok(seanceDtos);
        }

        [HttpPost]
        public async Task<ActionResult<SeanceDto>> PostSeance(SeanceCreateDto seanceDto)
        {
            var movie = await _context.Movies.FindAsync(seanceDto.MovieId);
            if (movie == null)
            {
                return BadRequest("Movie not found.");
            }

            var seance = new Seance
            {
                MovieId = seanceDto.MovieId,
                TheaterId = seanceDto.TheaterId,
                CinemaId = seanceDto.CinemaId, // Ustawiamy CinemaId
                StartTime = seanceDto.StartTime,
                EndTime = seanceDto.StartTime.AddMinutes(movie.Duration)
            };

            var overlappingSeance = await _context.Seances
                .Where(s => s.TheaterId == seance.TheaterId)
                .Where(s => s.StartTime < seance.EndTime && s.EndTime > seance.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingSeance != null)
            {
                return BadRequest("The seance overlaps with an existing seance.");
            }

            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();

            var resultDto = new SeanceDto
            {
                Id = seance.Id,
                MovieId = seance.MovieId,
                MovieTitle = movie.Title,
                TheaterId = seance.TheaterId,
                CinemaId = seance.CinemaId,
                PosterUrl = movie.PosterUrl,
                StartTime = seance.StartTime,
                EndTime = seance.EndTime
            };

            return CreatedAtAction(nameof(GetSeance), new { id = seance.Id }, resultDto);
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

            seance.EndTime = seance.StartTime.AddMinutes(movie.Duration);

            var overlappingSeance = await _context.Seances
                .Where(s => s.TheaterId == seance.TheaterId && s.Id != seance.Id)
                .Where(s => s.StartTime < seance.EndTime && s.EndTime > seance.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingSeance != null)
            {
                return BadRequest("The seance overlaps with an existing seance.");
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
