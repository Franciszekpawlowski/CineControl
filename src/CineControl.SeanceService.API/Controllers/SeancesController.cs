using CineControl.Common.Results;
using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs;
using CineControl.SeanceService.API.Models.DTOs.Seances;
using CineControl.SeanceService.API.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.SeanceService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class SeancesController : BaseController
    {
        private readonly ISeanceService _seanceService;

        public SeancesController(ISeanceService seanceService)
        {
            _seanceService = seanceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeances()
        {
            var result = await _seanceService.GetAllSeances();
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeance(int id)
        {
            var result = await _seanceService.GetSeanceById(id);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("bycinema/{cinemaId:int}/date/{date:datetime}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeancesByCinemaAndDate(int cinemaId, DateTime date)
        {
            var result = await _seanceService.GetSeancesByCinemaAndDate(cinemaId, date);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("bycinema/{cinemaId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeancesByCinema(int cinemaId)
        {
            var result = await _seanceService.GetSeancesByCinema(cinemaId);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddSeance([FromBody] SeanceCreateDto seanceCreateDto)
        {
            var result = await _seanceService.AddSeance(seanceCreateDto);
            return result.Match(
                onSuccess: Created,
                onFailure: Problem
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSeance(int id, [FromBody] SeanceDto seanceDto)
        {
            var result = await _seanceService.UpdateSeance(id, seanceDto);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSeance(int id)
        {
            var result = await _seanceService.DeleteSeance(id);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }
    }
}
