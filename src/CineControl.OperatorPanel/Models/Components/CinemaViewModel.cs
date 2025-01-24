using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineControl.OperatorPanel.Models.Components
{
    public class CinemaViewModel
    {
        public int id { get; set; }
        public List<SelectListItem> selectListItems { get; set; }
    }
}