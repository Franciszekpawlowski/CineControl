namespace CineControl.CinemaService.API.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public List<Theater> Theaters { get; set; } = new List<Theater>();
    }
}
