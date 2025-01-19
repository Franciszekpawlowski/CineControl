using CineControl.Common.Clients.CinemaService.Models.TheaterClient;

namespace CineControl.OperatorPanel.Models.DTOs.Theaters
{
    public class AddTheaterRequest
    {
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
        public int SeatsPerRow { get; set; }

    }
}
