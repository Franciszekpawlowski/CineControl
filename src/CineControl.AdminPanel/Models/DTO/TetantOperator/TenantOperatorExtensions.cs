using CineControl.Common.Clients.IdentityService.Models.Account;
using CineControl.Common.Clients.IdentityService.Models.Operator;

namespace CineControl.AdminPanel.Models.DTO.TetantOperator;

public static class TenantOperatorExtensions
{
    public static RegisterByAdminRequestModel ToRequest(this CreateOperatorRequest createOperatorRequest)
        => new() {
            TenantId = createOperatorRequest.TenantId,
            Username = createOperatorRequest.Username,
            Email = createOperatorRequest.Email,
            Password = createOperatorRequest.Password,
        };

    public static GetTenantOperatorResponse ToResponse (this GetUserResponseModel getUserResponse)
        => new() {
            Id = getUserResponse.UserId,
            Name = getUserResponse.Username,
            Email = getUserResponse.Email
        };

    public static List<GetTenantOperatorResponse> ToResponse (this IEnumerable<GetUserResponseModel> getUserResponses)
        => getUserResponses.Select(x => x.ToResponse()).ToList();
}