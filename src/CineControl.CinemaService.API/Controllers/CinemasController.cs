using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas()
        {
            return Ok(await _cinemaService.GetAllCinemas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cinema>> GetCinema(int id)
        {
            var cinema = await _cinemaService.GetCinemaById(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return Ok(cinema);
        }

        [HttpPost]
        public async Task<ActionResult<Cinema>> AddCinema(CreateCinemaRequest request)
        {
            await _cinemaService.AddCinema(
                request.Name,
                request.Address,
                request.City,
                request.State,
                request.ZipCode,
                request.TheaterConfigs
            );

            return CreatedAtAction(nameof(GetCinema), new { id = 1 }, null); // Assuming the first cinema is created
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCinema(int id, Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return BadRequest();
            }

            var existingCinema = await _cinemaService.GetCinemaById(id);
            if (existingCinema == null)
            {
                return NotFound();
            }

            await _cinemaService.UpdateCinema(cinema);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var cinema = await _cinemaService.GetCinemaById(id);
            if (cinema == null)
            {
                return NotFound();
            }

            await _cinemaService.DeleteCinema(id);
            return NoContent();
        }
    }
}

public class CreateCinemaRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public List<TheaterConfig> TheaterConfigs { get; set; }
}
