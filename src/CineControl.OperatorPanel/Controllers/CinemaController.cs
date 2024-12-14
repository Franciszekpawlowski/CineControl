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

        public async Task<ActionResult> AddCinema()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddCinema(AddCinemaRequest request)
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
            return View();
        }
    }
}
