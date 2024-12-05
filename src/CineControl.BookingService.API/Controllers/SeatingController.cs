using BookingService.API.Models.Results;
using BookingService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatingController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<SeatingController> _logger;

        public SeatingController(IReservationService reservationService, ILogger<SeatingController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpGet("seance/{seanceId}/reserved-seats")]
        public async Task<ActionResult<GenericResults<int[]>>> GetReservedSeats(int seanceId)
        {
            var result = new GenericResults<int[]>();

            // Zwracamy wyłącznie ID zarezerwowanych siedzeń dla danego seansu
            var reservedSeats = await _reservationService.GetReservedSeatsAsync(seanceId);

            if (reservedSeats == null || reservedSeats.Count == 0)
            {
                // Brak zarezerwowanych miejsc
                result.SetData(new int[0]);
                return Ok(result);
            }

            result.SetData(reservedSeats.ToArray());
            return Ok(result);
        }
    }
}
