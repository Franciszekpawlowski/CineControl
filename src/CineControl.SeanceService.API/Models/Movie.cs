namespace CineControl.SeanceService.API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; } 
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } 
        public string PosterUrl { get; set; }
        public int TenantID { get; set; }
        public string Genre { get; set; } 
        public double Rating { get; set; } 
    }
}
