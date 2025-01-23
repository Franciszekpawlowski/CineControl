using CineControl.OperatorPanel.Models.Components;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineControl.OperatorPanel.Components
{
    public class CinemaViewComponent(
        ICinemaService cinemaService
    ) : ViewComponent
    {
        public readonly ICinemaService _cinemaService = cinemaService;
        public async Task<IViewComponentResult> InvokeAsync() {
            var model = await _cinemaService.GetCinemasAsync();
            CinemaViewModel cinemaViewModel = new()
            {
                selectListItems = [.. model.Value.ToList().Select(
                    x => new SelectListItem {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }
                )]
            };
            return View("Default",cinemaViewModel);
        }
    }
}