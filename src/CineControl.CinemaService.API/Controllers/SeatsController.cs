using System;
using CineControl.CinemaService.API.Models;
using CineControl.CinemaService.API.Service.IService; 
using CineControl.Common.Enums;
using CineControl.CinemaService.API.Models.DTOs; 
using CineControl.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.CinemaService.API.Controllers
{
    [Route("api/v1/theaters/{theaterId:int}/seats")]
    [ApiController]
    //[Authorize(Policy = nameof(CustomPolicies.Operator))]
    public class SeatsController : BaseController
    {
        private readonly ICinemaService _cinemaService;

        public SeatsController(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSeats(int theaterId)
        {
            var result = await _cinemaService.GetSeatsByTheaterId(theaterId);
            return result.Match(
                onSuccess: seats => Ok(seats.Select(s => s.ToResponse())), 
                onFailure: Problem
            );
        }

        [HttpPost]
        public async Task<IActionResult> AddSeat(int theaterId, [FromBody] Seat seat)
        {
            var result = await _cinemaService.AddSeat(theaterId, seat);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }

        [HttpDelete("{seatId:int}")]
        public async Task<IActionResult> RemoveSeat(int theaterId, int seatId)
        {
            var result = await _cinemaService.RemoveSeat(theaterId, seatId);
            return result.Match(
                onSuccess: () => NoContent(),
                onFailure: Problem
            );
        }
    }
}
