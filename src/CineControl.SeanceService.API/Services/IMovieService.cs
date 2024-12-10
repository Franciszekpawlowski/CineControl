using CineControl.SeanceService.API.Models;
using CineControl.SeanceService.API.Models.DTOs.Movies;
using CineControl.Common.Results;

namespace CineControl.SeanceService.API.Service.IService
{
    public interface IMovieService
    {
        Task<ResultT<IEnumerable<Movie>>> GetAllMovies();
        Task<ResultT<Movie>> GetMovieById(int id);
        Task<ResultT<Movie>> AddMovie(AddMovieRequest request);
        Task<Result> UpdateMovie(Movie movie);
        Task<Result> DeleteMovie(int id);
    }
}
