using CineControl.OperatorPanel.Models.DTOs.Theaters;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Authorize]
    [Route("Cinema/{cinemaId:int}/theater")]
    public class TheaterController(
        ITheaterService theaterService) : Controller
    {
        private readonly ITheaterService _theaterService = theaterService;

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int cinemaId,Guid id)
        {
            var result = await _theaterService.GetTheaterByIdAsync(cinemaId,id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return View(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int cinemaId, Guid id, UpdateTheaterRequest request)
        {
            if (id != request.Id)
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
