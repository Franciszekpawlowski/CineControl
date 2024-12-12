using CineControl.Common.Clients.CinemaService.Models.GetCinemas;

namespace CineControl.OperatorPanel.Models.DTOs.Cinemas;

public class GetCinemaResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public GetCinemaResponse(GetCinemasResponseModel getCinemasResponseModel)
    {
        Id = getCinemasResponseModel.Id;
        Name = getCinemasResponseModel.Name;
        Address = getCinemasResponseModel.Address;
        City = getCinemasResponseModel.City;
        State = getCinemasResponseModel.State;
        ZipCode = getCinemasResponseModel.ZipCode;
    }
}