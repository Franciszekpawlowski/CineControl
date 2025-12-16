using CineControl.Common.Clients.SeanceService.Models.Movies;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.SeanceService.IClients;

public interface IMoviesClient
{
    public Task<ResultT<IEnumerable<GetMoviesResponseModel>>> GetMoviesAsync(string TenantId = null);
    public Task<ResultT<GetMoviesResponseModel>> GetMoviesByIdAsync(int id, string TenantId = null);
    public Task<Result> AddMovieAsync(AddMovieRequestModel addMovieRequestModel, string TenantId = null);
    public Task<Result> UpdateMovieAsync(int id, UpdateMovieRequestModel updateMovieRequestModel,string TenantId = null);
    public Task<Result> DeleteMovieAsync(int id,string TenantId = null);

}
