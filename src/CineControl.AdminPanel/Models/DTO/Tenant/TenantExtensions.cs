using CineControl.Common.Clients.TenantService.Models.Tenant;

namespace CineControl.AdminPanel.Models.DTO.Tenant;

public static class TenantExtensions
{
    public static GetTenantResponse ToResponse (this GetTenantResponseModel getTenantResponseModel)
        => new() {
            Id = getTenantResponseModel.Id,
            Name = getTenantResponseModel.Name,
            Description = getTenantResponseModel.Description
        };

    public static List<GetTenantResponse> ToResponse (this IEnumerable<GetTenantResponseModel> getTenantResponseModel)
        => getTenantResponseModel.Select(x => x.ToResponse()).ToList();

    public static UpdateTenantRequest ToUpdate (this GetTenantResponse getTenantResponse)
        => new() {
            Id = getTenantResponse.Id,
            Name = getTenantResponse.Name,
            Description = getTenantResponse.Description
        };

    public static CreateTenantRequestModel ToRequest (this CreateTenantRequest createTenantRequest)
        => new() {
            Name = createTenantRequest.Name,
            Description = createTenantRequest.Description
        };

    public static UpdateTenantRequestModel ToRequest (this UpdateTenantRequest updateTenantRequest)
        => new() {
            Name = updateTenantRequest.Name,
            Description = updateTenantRequest.Description
        };
}