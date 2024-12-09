using CineControl.CinemaService.API.Models.DTOs.Cinemas;
using CineControl.CinemaService.API.Models.DTOs;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Enums;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class CinemasController : BaseController
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCinemas()
        {
            var result = await _cinemaService.GetAllCinemas();
            return result.Match(
                onSuccess: cinemas => Ok(cinemas.Select(c => c.ToResponse())),
                onFailure: Problem
            );
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCinema(int id)
        {
            var result = await _cinemaService.GetCinemaById(id);
            return result.Match(
                onSuccess: cinema => Ok(cinema.ToResponse()),
                onFailure: Problem
            );
        }

        [HttpGet("cities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var result = await _cinemaService.GetAllCities();
            return result.Match(
                onSuccess: cities => Ok(new CitiesResponse { Cities = cities }),
                onFailure: Problem
            );
        }

        [HttpGet("ByCity/{city}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCinemasByCity(string city)
        {
            var result = await _cinemaService.GetCinemasByCity(city);
            return result.Match(
                onSuccess: cinemas => Ok(new CityCinemasResponse { City = city, Cinemas = cinemas.Select(c => c.ToResponse()) }),
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddCinema([FromBody] AddCinemaRequest request)
        {
            var result = await _cinemaService.AddCinema(request);
            return result.Match(
                onSuccess: cinema => CreatedAtAction(nameof(GetCinema), new { id = cinema.Id }, cinema.ToResponse()),
                onFailure: Problem
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCinema(int id, [FromBody] CinemaResponse updatedCinema)
        {
            var cinema = new Models.Cinema
            {
                Id = id,
                TenantId = updatedCinema.TenantId,
                Name = updatedCinema.Name,
                Address = updatedCinema.Address,
                City = updatedCinema.City,
                State = updatedCinema.State,
                ZipCode = updatedCinema.ZipCode
            };

            var result = await _cinemaService.UpdateCinema(cinema);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var result = await _cinemaService.DeleteCinema(id);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }
    }
}
