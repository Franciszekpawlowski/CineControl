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

    }
}
