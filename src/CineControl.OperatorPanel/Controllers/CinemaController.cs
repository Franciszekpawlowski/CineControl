using CineControl.OperatorPanel.Models.DTOs.Cinemas;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Authorize]
    public class CinemaController(ICinemaService cinemaService) : Controller
    {
        private readonly ICinemaService _cinemaService = cinemaService; 
        public async Task<ActionResult> Index()
        {
            var model = await _cinemaService.GetCinemasAsync();
            return View(model.Value);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddCinemaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _cinemaService.AddCinemaAsync(request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return View(request);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _cinemaService.GetCinemasByIdAsync(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return View(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, UpdateCinemaRequest request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _cinemaService.UpdateCinemaAsync(id, request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var result = await _cinemaService.GetCinemasByIdAsync(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Error", result.Error.ToString());
                return RedirectToAction("Index");
            }
            return View(result.Value);
        }
    }
}
