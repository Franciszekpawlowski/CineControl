using CineControl.BookingService.API.Models.DTOs.Reservations;
using CineControl.BookingService.API.Services;
using CineControl.Common.JWTProvider;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace CineControl.BookingService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class ReservationsController : BaseController
    {
        private readonly IReservationService _reservationService;
        private readonly IJWTProvider _jwtProvider;

        public ReservationsController(IReservationService reservationService, IJWTProvider jWTProvider)
        {
            _reservationService = reservationService;
            _jwtProvider = jWTProvider;
        }
        [HttpGet("MyReservations")]
        public async Task<IActionResult> GetUserReservations()
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            _jwtProvider.SetToken(token);

            var result = await _reservationService.GetUserReservationsAsync();
            return result.Match(
                onSuccess: (List<ReservationResponse> reservations) => Ok(reservations),
                onFailure: Problem
            );
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            _jwtProvider.SetToken(token);
            var result = await _reservationService.CreateReservationAsync(request);
            return result.Match(
                onSuccess: reservation => Ok(reservation),
                onFailure: Problem
            );
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            _jwtProvider.SetToken(token);
            var result = await _reservationService.CancelReservationAsync(reservationId);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }
    }
}
