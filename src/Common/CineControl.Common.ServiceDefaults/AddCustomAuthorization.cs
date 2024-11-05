using CineControl.Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CineControl.Common.ServiceDefaults;

public static class AddCustomAuthorization
{
    public static IHostApplicationBuilder AddAuthorization(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(CustomPolicies.Admin.ToString(), policy =>
                policy.RequireClaim(CustomClaims.Role, Roles.Admin.ToString())

                );

            options.AddPolicy(CustomPolicies.Operator.ToString(), policy =>
                policy.RequireClaim(CustomClaims.Role, Roles.Operator.ToString()));
        }
        );
        return builder;
    }
}
