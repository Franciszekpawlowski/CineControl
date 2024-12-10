using CineControl.CinemaService.API.Models.DTOs;
using CineControl.CinemaService.API.Models.DTOs.Theaters;
using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/v1/cinemas/{cinemaId:int}/theaters")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class TheatersController(ITheaterServices theaterService) : BaseController
    {
        private readonly ITheaterServices _theaterService = theaterService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheaters(int cinemaId)
        {
            var result = await _theaterService.GetTheatersByCinemaId(cinemaId);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpGet("~/api/theaters/{theaterId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheaterById(int theaterId)
        {
            var result = await _theaterService.GetTheaterById(theaterId);
            return result.Match(
                onSuccess: Ok,
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddTheater(int cinemaId, [FromBody] AddTheaterRequest request)
        {
            var result = await _theaterService.AddTheater(cinemaId, request);
            return result.Match(
                onSuccess: NoContent,
                onFailure: Problem
            );
        }


        [HttpDelete("{theaterId:int}")]
        public async Task<IActionResult> RemoveTheater(int cinemaId, int theaterId)
        {
            var result = await _theaterService.RemoveTheater(cinemaId, theaterId);
            return result.Match(
                onSuccess: NoContent,
                onFailure: Problem
            );
        }
    }
}
