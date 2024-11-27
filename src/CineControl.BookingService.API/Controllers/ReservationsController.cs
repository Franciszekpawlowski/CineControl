using BookingService.API.Models.Request;
using BookingService.API.Models.Response;
using BookingService.API.Models.Results;
using BookingService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(IReservationService reservationService, ILogger<ReservationsController> logger)
        {
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<GenericResults<ReservationResponse>>> CreateReservation([FromBody] ReservationRequest request)
        {
            var result = await _reservationService.CreateReservationAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
