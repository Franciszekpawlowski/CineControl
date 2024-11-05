using System.Text;
using CineControl.Common.JWT.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CineControl.Common.ServiceDefaults
{
    public static class AddJWTAuth
    {
        public static IHostApplicationBuilder AddJwtAuth(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtConfig"));

            var _options = builder.GetJwtOptions();
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _options.Issuer,
                        ValidAudience = _options.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret))
                    };
                });
            return builder;
        }

        internal static JwtOptions GetJwtOptions(this IHostApplicationBuilder builder) => 
            builder.Configuration.GetSection("JwtConfig").Get<JwtOptions>() ?? throw new KeyNotFoundException("JwtConfig");
    }
}