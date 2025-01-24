using CineControl.Common.Clients.CinemaService.Models.CinemaClient;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.CinemaService.IClients;

public interface ICinemaClient
{
    Task<ResultT<IEnumerable<GetCinemasResponseModel>>> GetCinemasAsync(string TenantId = null);
    Task<ResultT<GetCinemasResponseModel>> GetCinemaAsync(int id,string TenantId = null);
    Task<ResultT<GetCinemasResponseModel>> GetCinemaByCity(string city,string TenantId = null);
    Task<Result> AddCinemaAsync(AddCinemaRequestModel model,string TenantId = null);
    Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequestModel model, string TenantId = null);
    Task<Result> DeleteCinemaAsync(int id, string TenantId = null);
}
