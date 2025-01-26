using CineControl.OperatorPanel.Models.DTOs.Theaters;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Authorize]
    [Route("Cinema/{cinemaId}/[controller]")]
    public class TheaterController(
        ITheaterService theaterService) : Controller
    {
        private readonly ITheaterService _theaterService = theaterService;

        [HttpGet("Create")]
        public ActionResult Create(
            int cinemaId
        )
        {
            ViewBag.cinemaId = cinemaId;
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int cinemaId,AddTheaterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _theaterService.AddTheaterAsync(cinemaId,request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return View(request);
            }
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit(int cinemaId,int id)
        {
            var result = await _theaterService.GetTheaterByIdAsync(cinemaId,id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
            }
            UpdateTheaterRequest updateTheaterRequest = new()
            {
                Id = result.Value.Id,
                CinemaId = cinemaId,
                Name = result.Value.Name,
                SeatingCapacity = result.Value.SeatingCapacity,
                SeatsPerRow = result.Value.SeatsPerRow,
            };
            return View(updateTheaterRequest);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int cinemaId, int id, UpdateTheaterRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }
            if (cinemaId != request.CinemaId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _theaterService.UpdateTheaterAsync(cinemaId,id, request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
            }
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
        }

        [HttpGet("Delete/{id}")]
        public async Task<ActionResult> Delete(int cinemaId, int id)
        {
            var result = await _theaterService.GetTheaterByIdAsync(cinemaId,id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
            }
            result.Value.CinemaId = cinemaId;
            return View(result.Value);
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> DeleteConfirmed(int cinemaId, int id)
        {
            var result = await _theaterService.DeleteTheaterAsync(cinemaId,id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
            }
            return RedirectToAction("Details", "Cinema", new { id = cinemaId });
        }

    }
}
