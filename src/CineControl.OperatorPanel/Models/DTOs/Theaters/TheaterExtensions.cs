using CineControl.Common.Clients.CinemaService.Models.TheaterClient;

namespace CineControl.OperatorPanel.Models.DTOs.Theaters;

public static class TheaterExtensions
{
    public static GetTheaterResponse ToResponse(this GetTheaterResponseModel getTheaterResponseModel)
        => new(){
            Id = getTheaterResponseModel.Id,
            Name = getTheaterResponseModel.Name,
            SeatingCapacity = getTheaterResponseModel.SeatingCapacity,
            SeatsPerRow = getTheaterResponseModel.SeatsPerRow
        };

    public static List<GetTheaterResponse> ToResponse(this IEnumerable<GetTheaterResponseModel> getTheaterResponseModels)
        => getTheaterResponseModels.Select(x => x.ToResponse()).ToList();


    public static AddTheaterRequestModel ToRequest(this AddTheaterRequest addTheaterRequest)
        => new()
        {
            Name = addTheaterRequest.Name,
            SeatingCapacity = addTheaterRequest.SeatingCapacity,
            SeatsPerRow = addTheaterRequest.SeatsPerRow
        };

    public static UpdateTheaterRequestModel ToRequest(this UpdateTheaterRequest addTheaterRequest)
        => new()
        {
            Name = addTheaterRequest.Name,
            SeatingCapacity = addTheaterRequest.SeatingCapacity,
            SeatsPerRow = addTheaterRequest.SeatsPerRow
        };
}