using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CineControl.Common;
using CineControl.Common.Options;
using CineControl.IdentityService.API.Models;
using CineControl.IdentityService.API.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CineControl.IdentityService.API.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser applicationUser)
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey key = new(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

            var UserClaims = await _userManager.GetClaimsAsync(applicationUser);

            var AdditionalClaimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName),
                new Claim(CustomClaims.UserId,applicationUser.Id.ToString()),
                new Claim(CustomClaims.TenantId,applicationUser.TenantId.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Name,applicationUser.UserName),
                new Claim(ClaimTypes.Email,applicationUser.Email)
            };
            AdditionalClaimList.AddRange(UserClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = new ClaimsIdentity(AdditionalClaimList),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var principal = tokenhandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
            {
                return null;
            }
            return principal;
        }


        public ClaimsPrincipal? GetPrincipalFromToken(string? token)
        {
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var principal = tokenhandler.ValidateToken(token, TokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                )
            {
                return null;
            }
            return principal;
        }
    }
}