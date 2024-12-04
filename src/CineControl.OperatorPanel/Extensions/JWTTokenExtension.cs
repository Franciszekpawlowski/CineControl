// using System.Text;
// using CineControl.Common.Options;

// namespace CineControl.OperatorPanel.Extensions
// {
//     public static class JWTTokenExtension
//     {
//         public static IHostApplicationBuilder AddJwtAuthExtension(this IHostApplicationBuilder builder)
//         {
//             builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtConfig"));

//             var _options = builder.GetJwtOptions();
//             builder.Services.AddAuthentication(o =>
//             {
//                 o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                 o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//                 o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//             })
//                 .AddJwtBearer(options =>
//                 {
//                     options.TokenValidationParameters = new TokenValidationParameters
//                     {
//                         ValidateIssuer = true,
//                         ValidateAudience = true,
//                         ValidateLifetime = true,
//                         ValidateIssuerSigningKey = true,
//                         ValidIssuer = _options.Issuer,
//                         ValidAudience = _options.Audience,
//                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret))
//                     };
//                     options.Events = new JwtBearerEvents
//                     {
//                         OnMessageReceived = context =>
//                         {
//                             var accessToken = context.Request.Query["access_token"];
//                             if (!string.IsNullOrEmpty(accessToken))
//                             {
//                                 context.Token = accessToken;
//                             }
//                             return Task.CompletedTask;
//                         }
//                     };
//                 });
//             return builder;
//         }

//         static JwtOptions GetJwtOptions(this IHostApplicationBuilder builder) =>
//             builder.Configuration.GetSection("JwtConfig").Get<JwtOptions>() ?? throw new KeyNotFoundException("JwtConfig");
//     }
// }