using System;
using CineControl.Common.Clients.AuthService.Models.Login;

namespace CineControl.OperatorPanel.Extensions;

public static class HTTPContextExtension
{
    public static HttpContext AddCookies(this HttpContext httpContext, 
        LoginResponseModel loginResponseModel
    )
    {
        httpContext.Response.Cookies.Append("Token", loginResponseModel.Token,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true
            });

        httpContext.Response.Cookies.Append("RefreshToken", loginResponseModel.RefreshToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true
            });
        // httpContext.Response.Cookies.Append("ExpiresIn", loginResponseModel.ExpiresIn.ToString(),
        //     new CookieOptions
        //     {
        //         Expires = DateTimeOffset.UtcNow.AddDays(1),
        //         HttpOnly = true
        //     });

        return httpContext;
    }
}
