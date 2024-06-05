namespace CineControl.FilmService.API.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } // in minutes
        public string PosterUrl { get; set; }
        public int tenantID {get; set;}
    }
}
