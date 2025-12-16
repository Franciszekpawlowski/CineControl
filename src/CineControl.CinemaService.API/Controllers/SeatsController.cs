using CineControl.CinemaService.API.Service.IService;
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/v1/cinemas/{cinemaId:int}/theaters/{theaterId:int}/seats")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class SeatsController(ISeatService seatService) : BaseController
    {
        private readonly ISeatService _seatService = seatService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeats(int cinemaId,int theaterId)
        {
            var result = await _seatService.GetSeatsByTheaterId(cinemaId,theaterId);
            return result.Match(
                onSuccess: Ok, 
                onFailure: Problem
            );
        }
    }
}
