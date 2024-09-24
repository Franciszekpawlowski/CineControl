using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/theaters/{theaterId}/seats")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public SeatsController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeats(int theaterId)
        {
            return Ok(await _cinemaService.GetSeatsByTheaterId(theaterId));
        }

        [HttpPost]
        public async Task<ActionResult<Seat>> AddSeat(int theaterId, Seat seat)
        {
            await _cinemaService.AddSeat(theaterId, seat);
            return CreatedAtAction(nameof(GetSeats), new { theaterId }, seat);
        }

        [HttpDelete("{seatId}")]
        public async Task<IActionResult> RemoveSeat(int theaterId, int seatId)
        {
            await _cinemaService.RemoveSeat(theaterId, seatId);
            return NoContent();
        }
    }
}
