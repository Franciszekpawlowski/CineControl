using CineControl.AdminPanel.Models.DTO.Tenant;
using CineControl.AdminPanel.Models.DTO.TetantOperator;

namespace CineControl.AdminPanel.Models.DTO;

public class TenantOperatorModelView
{
    public GetTenantResponse Tenant { get; set; }
    public IEnumerable<GetTenantOperatorResponse> TenantOperator { get; set; }
}