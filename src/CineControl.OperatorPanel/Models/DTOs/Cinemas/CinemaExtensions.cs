using CineControl.Common.Clients.CinemaService.Models.CinemaClient;

namespace CineControl.OperatorPanel.Models.DTOs.Cinemas;

public static class CinemaExtensions
{
    public static GetCinemaResponse ToResponse (this GetCinemasResponseModel getCinemasResponseModel) 
        => new(){
            Id = getCinemasResponseModel.Id,
            Name = getCinemasResponseModel.Name,
            Address = getCinemasResponseModel.Address,
            City = getCinemasResponseModel.City,
            State = getCinemasResponseModel.State,
            ZipCode = getCinemasResponseModel.ZipCode
        };

    public static List<GetCinemaResponse> ToResponse (this IEnumerable<GetCinemasResponseModel> getCinemasResponseModel) 
        =>  getCinemasResponseModel.Select(x => x.ToResponse()).ToList();

    public static AddCinemaRequestModel ToRequest (this AddCinemaRequest addCinemaRequest) 
        => new() {
            Name = addCinemaRequest.Name,
            Address = addCinemaRequest.Address,
            City = addCinemaRequest.City,
            State = addCinemaRequest.State,
            ZipCode = addCinemaRequest.ZipCode
        };

    public static UpdateCinemaRequestModel ToRequest (this UpdateCinemaRequest updateCinemaRequest)
        => new()
        {
            Name = updateCinemaRequest.Name,
            Address = updateCinemaRequest.Address,
            City = updateCinemaRequest.City,
            State = updateCinemaRequest.State,
            ZipCode = updateCinemaRequest.ZipCode
        };
}
