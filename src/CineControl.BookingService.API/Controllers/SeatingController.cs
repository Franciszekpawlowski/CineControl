using BookingService.API.Models;
using BookingService.API.Models.Results;
using BookingService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatingController : ControllerBase
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IReservationService _reservationService;
        private readonly ILogger<SeatingController> _logger;

        public SeatingController(IExternalApiService externalApiService, IReservationService reservationService, ILogger<SeatingController> logger)
        {
            _externalApiService = externalApiService;
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpGet("seance/{seanceId}")]
        public async Task<ActionResult<GenericResults<Theater>>> GetSeatingStatus(int seanceId)
        {
            var result = new GenericResults<Theater>();

            // Pobierz seans z zewnętrznego API
            var seanceDto = await _externalApiService.GetSeanceAsync(seanceId);
            if (seanceDto == null)
            {
                result.AddError("Seans nie został znaleziony.");
                return NotFound(result);
            }

            // Pobierz teatr z zewnętrznego API
            var theater = await _externalApiService.GetTheaterAsync(seanceDto.TheaterId);
            if (theater == null)
            {
                result.AddError("Teatr nie został znaleziony.");
                return NotFound(result);
            }

            // Sprawdź, które siedzenia są zarezerwowane
            var reservedSeats = await _reservationService.GetReservedSeatsAsync(seanceId);

            // Oznacz siedzenia jako zarezerwowane lub dostępne
            foreach (var seat in theater.Seats)
            {
                seat.IsReserved = reservedSeats.Contains(seat.Id);
            }

            result.SetData(theater);
            return Ok(result);
        }
    }
}
