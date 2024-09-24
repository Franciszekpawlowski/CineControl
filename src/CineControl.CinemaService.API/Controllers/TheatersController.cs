using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/theaters")]
    [ApiController]
    public class TheatersController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public TheatersController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetTheaters(int cinemaId)
        {
            return Ok(await _cinemaService.GetTheatersByCinemaId(cinemaId));
        }

        [HttpPost]
        public async Task<ActionResult<Theater>> AddTheater(int cinemaId, Theater theater)
        {
            await _cinemaService.AddTheater(cinemaId, theater);
            return CreatedAtAction(nameof(GetTheaters), new { cinemaId }, theater);
        }

        [HttpDelete("{theaterId}")]
        public async Task<IActionResult> RemoveTheater(int cinemaId, int theaterId)
        {
            await _cinemaService.RemoveTheater(cinemaId, theaterId);
            return NoContent();
        }
    }
}
