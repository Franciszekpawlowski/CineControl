using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CineControl.Common.JWTProvider;

public class JWTProvider : IJWTProvider
{
    private string _Token;
    private readonly JwtSecurityTokenHandler _securityTokenHandler;
    public JWTProvider()
    {
        _securityTokenHandler = new JwtSecurityTokenHandler();
    }
    public IEnumerable<Claim> GetClaims()
    {
        var jwtToken = _securityTokenHandler.ReadJwtToken(_Token);
        return jwtToken.Claims;
    }

    public void SetToken(string token) => _Token = token;

    public string GetTenantId() 
        => GetClaims().FirstOrDefault(c => c.Type == CustomClaims.TenantId).Value;


}
