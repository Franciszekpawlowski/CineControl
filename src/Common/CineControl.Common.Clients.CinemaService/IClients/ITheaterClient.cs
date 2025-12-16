using CineControl.Common.Clients.CinemaService.Models.TheaterClient;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.CinemaService.IClients;

public interface ITheaterClient
{
    Task<ResultT<IEnumerable<GetTheaterResponseModel>>> GetTheatersAsync(int cinemaId,string? TenantId = null);
    Task<ResultT<GetTheaterResponseModel>> GetTheaterByIdAsync(int cinemaId,int theaterId,string? TenantId = null);
    Task<Result> AddTheaterAsync(int cinemaId, AddTheaterRequestModel model,string? TenantId = null);
    Task<Result> UpdateTheaterAsync(int cinemaId, int theaterId, UpdateTheaterRequestModel model,string? TenantId = null);
    Task<Result> DeleteTheaterAsync(int cinemaId, int theaterId, string? TenantId = null);
}