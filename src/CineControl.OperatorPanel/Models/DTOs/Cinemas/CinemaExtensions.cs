using CineControl.Common.Clients.CinemaService.Models.GetCinemas;

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
}
