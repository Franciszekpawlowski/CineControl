namespace CineControl.CinemaService.API.Models.DTOs.Cinemas;

public static class CinemaMappingExtensions
{
    public static CinemaResponse ToResponse(this Cinema cinema)
    {
        return new CinemaResponse
        {
            Id = cinema.Id,
            Name = cinema.Name,
            Address = cinema.Address,
            City = cinema.City,
            State = cinema.State,
            ZipCode = cinema.ZipCode,
            //Theaters = cinema.Theaters.Select(t => t.ToResponse()).ToList()
        };
    }

    public static List<CinemaResponse> ToResponse(this List<Cinema> cinemas)
    {
        return cinemas.Select(ToResponse).ToList();
    }

    public static GetCinemasByCityResponse ToResponse(
        this List<string> cities
    )
    {
        return new GetCinemasByCityResponse
        {
            Cities = cities
        };
    }
}
