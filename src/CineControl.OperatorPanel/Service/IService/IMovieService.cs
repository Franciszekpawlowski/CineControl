using CineControl.Common.Results;
using CineControl.OperatorPanel.Models.DTOs.Movie;

namespace CineControl.OperatorPanel.Service.IService;

public interface IMovieService
{
    Task<ResultT<IEnumerable<GetMovieResponse>>> GetMoviesAsync();
    Task<ResultT<GetMovieResponse>> GetMovieByIdAsync(int id);
    Task<Result> AddMovieAsync(AddMovieRequest request);
    Task<Result> UpdateMovieAsync(int id, UpdateMovieRequest request);
    Task<Result> DeleteMovieAsync(int id);
}
