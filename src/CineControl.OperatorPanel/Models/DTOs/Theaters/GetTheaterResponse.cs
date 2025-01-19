namespace CineControl.OperatorPanel.Models.DTOs.Theaters
{
    public class GetTheaterResponse()
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SeatingCapacity { get; set; }
    }
}