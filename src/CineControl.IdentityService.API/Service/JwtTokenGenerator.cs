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

            var claimsList = await _userManager.GetClaimsAsync(applicationUser);
            var claim = claimsList.FirstOrDefault(claim => claim.Type == CustomClaims.Role);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName),
            };

            if (claim is not null)
            {
                claimList.Add(claim);
            }

            // claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Subject = new ClaimsIdentity(claimList),
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

        public ClaimsPrincipal? GetTokenPrincipal(string token)
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out _);
        }
    }
}