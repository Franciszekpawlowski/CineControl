namespace CineControl.OperatorPanel.Models.DTOs.Theaters
{
    public class UpdateTheaterRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
    }
}
