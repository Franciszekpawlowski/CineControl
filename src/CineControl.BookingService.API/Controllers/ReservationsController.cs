using CineControl.BookingService.API.Models.DTOs.Reservations;
using CineControl.BookingService.API.Services;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CineControl.BookingService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class ReservationsController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
        {
            var result = await _reservationService.CreateReservationAsync(request);
            return result.Match(
                onSuccess: reservation => Ok(reservation),
                onFailure: Problem
            );
        }
    }
}
