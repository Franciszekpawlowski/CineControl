namespace CineControl.OperatorPanel.Models.DTOs.Theaters
{
    public class UpdateTheaterRequest
    {
        public int Id { get; set; }
        public int CinemaId { get; set; }
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
        public int SeatsPerRow { get; set; }
    }
}
