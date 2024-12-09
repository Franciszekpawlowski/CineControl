using CineControl.Common.Clients.CinemaService.Models.AddCinema;
using CineControl.Common.Clients.CinemaService.Models.GetCinemas;
using CineControl.Common.Clients.CinemaService.Models.UpdateCinema;
using CineControl.Common.Results;

namespace CineControl.Common.Clients.CinemaService.IClients;

public interface ICinemaServiceClients
{
    Task<ResultT<IEnumerable<GetCinemasResponseModel>>> GetCinemasAsync();
    Task<ResultT<GetCinemasResponseModel>> GetCinemaAsync(int id);
    Task<ResultT<GetCinemasResponseModel>> GetCinemaByCity(string city);
    Task<Result> AddCinemaAsync(AddCinemaRequestModel model);
    Task<Result> UpdateCinemaAsync(int id, UpdateCinemaRequestModel model);
    Task<Result> DeleteCinemaAsync(int id);
}
