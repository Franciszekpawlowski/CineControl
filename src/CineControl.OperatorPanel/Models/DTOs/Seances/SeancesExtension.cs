using CineControl.Common.Clients.SeanceService.Models.Seances;

namespace CineControl.OperatorPanel.Models.DTOs.Seances;

public static class SeancesExtensions
{
    public static GetSeanceResponse ToResponse(this GetSeancesResponseModel getSeancesResponseModel)
        => new(){
            Id = getSeancesResponseModel.Id,
            MovieId = getSeancesResponseModel.MovieId,
            MovieTitle = getSeancesResponseModel.MovieTitle,
            TheaterId = getSeancesResponseModel.TheaterId,
            CinemaId = getSeancesResponseModel.CinemaId,
            PosterUrl = getSeancesResponseModel.PosterUrl,
            PosterBase64 = getSeancesResponseModel.PosterBase64,
            PosterBase64Thumbnail = string.Format("data:image/jpg;base64, {0}", getSeancesResponseModel.PosterBase64),
            StartTime = getSeancesResponseModel.StartTime,
            EndTime = getSeancesResponseModel.EndTime
        };

    public static List<GetSeanceResponse> ToResponse(this IEnumerable<GetSeancesResponseModel> getSeancesResponseModel)
        => getSeancesResponseModel.Select(x => x.ToResponse()).ToList();

    public static AddSeanceRequestModel ToRequest(this AddSeanceRequest addSeanceRequest)
        => new(){
            MovieId = addSeanceRequest.MovieId,
            TheaterId = addSeanceRequest.TheaterId,
            CinemaId = addSeanceRequest.CinemaId,
            StartTime = addSeanceRequest.StartTime
        };

    public static UpdateSeanceRequestModel ToRequest(this UpdateSeanceRequest updateSeanceRequest)
        => new(){
            Id = updateSeanceRequest.Id,
            MovieId = updateSeanceRequest.MovieId,
            TheaterId = updateSeanceRequest.TheaterId,
            CinemaId = updateSeanceRequest.CinemaId,
            StartTime = updateSeanceRequest.StartTime
        };
}