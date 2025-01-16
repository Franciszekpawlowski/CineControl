using System;
using System.Security.Claims;

namespace CineControl.Common.JWTProvider;

public interface IJWTProvider
{
    public IEnumerable<Claim> GetClaims();
    void SetToken(string token);
    public string GetTenantId();
    public string GetUserId();
}
