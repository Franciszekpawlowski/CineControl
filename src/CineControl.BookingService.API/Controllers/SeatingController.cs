using CineControl.BookingService.API.Services;
using BookingService.API.Models.DTOs;
using CineControl.BookingService.API.Models.DTOs.Seatings;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CineControl.BookingService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class SeatingController : BaseController
    {
        private readonly IReservationService _reservationService;

        public SeatingController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("seance/{seanceId}/reserved-seats")]
        public async Task<IActionResult> GetReservedSeats(int seanceId)
        {
            var result = await _reservationService.GetReservedSeatsAsync(seanceId);
            return result.Match(
                onSuccess: seats => Ok(seats.ToReservedSeatsResponse()),
                onFailure: Problem
            );
        }
    }
}
