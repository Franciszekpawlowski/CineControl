using System.Threading.Tasks;
using CineControl.Common.Clients.SeanceService.Models.Movies;
using CineControl.OperatorPanel.Providers;

namespace CineControl.OperatorPanel.Models.DTOs.Movie;

public static class MovieExtensions
{
    public static GetMovieResponse ToResponse(this GetMoviesResponseModel getMoviesResponseModel)
        => new(){
            Id = getMoviesResponseModel.Id,
            Title = getMoviesResponseModel.Title,
            Description = getMoviesResponseModel.Description,
            ShortDescription = getMoviesResponseModel.ShortDescription,
            ReleaseDate = getMoviesResponseModel.ReleaseDate,
            Duration = getMoviesResponseModel.Duration,
            PosterUrl = getMoviesResponseModel.PosterUrl,
            PanoramicPosterUrl = getMoviesResponseModel.PanoramicPosterUrl,
            Genre = getMoviesResponseModel.Genre,
            Rating = getMoviesResponseModel.Rating
        };

    public static List<GetMovieResponse> ToResponse(this IEnumerable<GetMoviesResponseModel> getMoviesResponseModel)
        => getMoviesResponseModel.Select(x => x.ToResponse()).ToList();

    public static AddMovieRequestModel ToRequest(this AddMovieRequest addMovieRequest)
        => new(){
            Title = addMovieRequest.Title,
            Description = addMovieRequest.Description,
            ShortDescription = addMovieRequest.ShortDescription,
            ReleaseDate = addMovieRequest.ReleaseDate,
            Duration = addMovieRequest.Duration,
            PosterUrl = addMovieRequest.PosterUrl,
            PosterB64 = FileProvider.ConvertToBase64(addMovieRequest.PosterFile),
            PanoramicPosterUrl = addMovieRequest.PanoramicPosterUrl,
            PanoramicPosterB64 = FileProvider.ConvertToBase64(addMovieRequest.PanoramicPosterFile),
            Genre = addMovieRequest.Genre,
            Rating = addMovieRequest.Rating
        };

    public static UpdateMovieRequestModel ToRequest(this UpdateMovieRequest updateMovieRequest)
        => new(){
            Title = updateMovieRequest.Title,
            Description = updateMovieRequest.Description,
            ShortDescription = updateMovieRequest.ShortDescription,
            ReleaseDate = updateMovieRequest.ReleaseDate,
            Duration = updateMovieRequest.Duration,
            PosterUrl = updateMovieRequest.PosterUrl,
            PanoramicPosterUrl = updateMovieRequest.PanoramicPosterUrl,
            Genre = updateMovieRequest.Genre,
            Rating = updateMovieRequest.Rating
        };
}