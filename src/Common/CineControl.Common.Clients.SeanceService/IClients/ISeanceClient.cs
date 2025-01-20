using CineControl.Common.Clients.SeanceService.Models.Seances;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.SeanceService.IClients;

public interface ISeanceClient
{
    public Task<ResultT<IEnumerable<GetSeancesResponseModel>>> GetMoviesAsync(string TenantId = null);
    public Task<ResultT<GetSeancesResponseModel>> GetMoviesByIdAsync(int id, string TenantId = null);
    public Task<Result> AddSeanceAsync(AddSeanceRequestModel addSeanceRequestModel, string TenantId = null);
    public Task<Result> UpdateSeanceAsync(int id, UpdateSeanceRequestModel updateSeanceRequestModel, string TenantId = null);
    public Task<Result> DeleteSeanceAsync(int id, string TenantId = null);

}
