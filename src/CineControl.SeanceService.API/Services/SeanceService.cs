using CineControl.Common.Results;
using CineControl.Common.Tenant;
using CineControl.SeanceService.API.Data;
using CineControl.SeanceService.API.Errors;
using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Seances;
using CineControl.SeanceService.API.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CineControl.SeanceService.API.Service
{
    public class SeanceService : ISeanceService
    {
        private readonly AppDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public SeanceService(AppDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<ResultT<IEnumerable<Seance>>> GetAllSeances()
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var seances = await _context.Seances
                .Include(s => s.Movie)
                .ToListAsync();
            return seances;
        }

        public async Task<ResultT<Seance>> GetSeanceById(int id)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var seance = await _context.Seances
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == id);
            return seance != null
                ? seance
                : SeanceErrors.NotFound($"Seance with id {id} not found");
        }

        public async Task<ResultT<IEnumerable<Seance>>> GetSeancesByCinemaAndDate(int cinemaId, DateTime date)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            var seances = await _context.Seances
                .Include(s => s.Movie)
                .Where(s => s.CinemaId == cinemaId && s.StartTime.Date == utcDate.Date)
                .ToListAsync();

            return seances;
        }

        public async Task<ResultT<Seance>> AddSeance(SeanceCreateDto seanceCreateDto)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            // Walidacja filmu
            var movie = await _context.Movies.FindAsync(seanceCreateDto.MovieId);
            if (movie == null)
            {
                return SeanceErrors.NotFound($"Movie with id {seanceCreateDto.MovieId} not found.");
            }

            var seance = new Seance
            {
                MovieId = seanceCreateDto.MovieId,
                TenantId = _tenantProvider.GetTenantId(),
                TheaterId = seanceCreateDto.TheaterId,
                CinemaId = seanceCreateDto.CinemaId,
                StartTime = DateTime.SpecifyKind(seanceCreateDto.StartTime, DateTimeKind.Utc),
                EndTime = DateTime.SpecifyKind(seanceCreateDto.StartTime.AddMinutes(movie.Duration), DateTimeKind.Utc)
            };

            // Sprawdzenie nakładania się seansów
            var overlappingSeance = await _context.Seances
                .Where(s => s.TheaterId == seance.TheaterId)
                .Where(s => s.StartTime < seance.EndTime && s.EndTime > seance.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingSeance != null)
            {
                return SeanceErrors.Conflict("The seance overlaps with an existing seance.");
            }

            _context.Seances.Add(seance);
            await _context.SaveChangesAsync();
            return seance;
        }

        public async Task<Result> UpdateSeance(int id, SeanceDto seanceDto)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            if (id != seanceDto.Id)
            {
                return SeanceErrors.UnprocessableEntity("Seance ID mismatch.");
            }

            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return SeanceErrors.NotFound($"Seance with id {id} not found.");
            }
            if(seance.TenantId != _tenantProvider.GetTenantId()){
                return SeanceErrors.UnprocessableEntity("Tenant ID mismatch.");
            }

            var movie = await _context.Movies.FindAsync(seanceDto.MovieId);
            if (movie == null)
            {
                return SeanceErrors.NotFound($"Movie with id {seanceDto.MovieId} not found.");
            }

            seance.MovieId = seanceDto.MovieId;
            seance.TheaterId = seanceDto.TheaterId;
            seance.CinemaId = seanceDto.CinemaId;
            seance.StartTime = DateTime.SpecifyKind(seanceDto.StartTime, DateTimeKind.Utc);
            seance.EndTime = DateTime.SpecifyKind(seanceDto.StartTime.AddMinutes(movie.Duration), DateTimeKind.Utc);

            // Sprawdzenie nakładania się seansów
            var overlappingSeance = await _context.Seances
                .Where(s => s.TheaterId == seance.TheaterId && s.Id != seance.Id)
                .Where(s => s.StartTime < seance.EndTime && s.EndTime > seance.StartTime)
                .FirstOrDefaultAsync();

            if (overlappingSeance != null)
            {
                return SeanceErrors.Conflict("The seance overlaps with an existing seance.");
            }

            _context.Seances.Update(seance);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(id))
                {
                    return SeanceErrors.NotFound($"Seance with id {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return Result.Success();
        }

        public async Task<Result> DeleteSeance(int id)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var seance = await _context.Seances.FindAsync(id);
            if (seance == null)
            {
                return SeanceErrors.NotFound($"Seance with id {id} not found.");
            }

            _context.Seances.Remove(seance);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        private bool SeanceExists(int id)
        {
            return _context.Seances.Any(e => e.Id == id);
        }

        public async Task<ResultT<IEnumerable<Seance>>> GetSeancesByCinema(int cinemaId)
        {
            if (!_tenantProvider.HasTenant())
            {
                return SeanceErrors.AccessUnauthorized("No tenant specified");
            }

            var seances = await _context.Seances
                .Include(s => s.Movie)
                .Where(s => s.CinemaId == cinemaId)
                .ToListAsync();

            return seances;
        }
    }
}
