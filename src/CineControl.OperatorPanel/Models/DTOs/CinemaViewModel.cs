using CineControl.OperatorPanel.Models.DTOs.Cinemas;
using CineControl.OperatorPanel.Models.DTOs.Theaters;

namespace CineControl.OperatorPanel.Models.DTOs
{
    public class CinemaViewModel
    {
        public GetCinemaResponse Cinema { get; set; }
        public IEnumerable<GetTheaterResponse> Theaters { get; set; }
    }
}