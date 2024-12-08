using System.Net;
using System.Security.Claims;
using CineControl.Common.Clients.IdentityService.Errors;
using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.Clients.IdentityService.Models.Login;
using CineControl.Common.Results;
using CineControl.OperatorPanel.Errors;
using CineControl.OperatorPanel.Extensions;
using CineControl.OperatorPanel.Models.GetUser;
using CineControl.OperatorPanel.Models.UserLogin;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CineControl.OperatorPanel.Service;

public class AuthService(IIdentityServiceClient authServiceClient) : IAuthService
{
    private readonly IIdentityServiceClient _authServiceClient = authServiceClient;

    public async Task<Result> LoginAsync(UserLoginRequest userRequest, HttpContext context)
    {
        
        var loginRequestModel = new LoginRequestModel() {
            Username = userRequest.Username,
            Password = userRequest.Password
        };
        var loginResponseModel = await _authServiceClient.LoginAsync(loginRequestModel);
        if (!loginResponseModel.IsSuccess)
        {
            return AuthServiceErrors.AccessUnauthorized;
        }
        context.AddCookies(loginResponseModel.Value!);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, userRequest.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

        var authenticationProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await context.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authenticationProperties
        );

        return Result.Success();
    }

    public async Task<ResultT<GetUserResult>> GetUserAsync(HttpContext context)
    {
        string token = context.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return ClientErrors.AccessUnauthorized;
        }

        var result = await _authServiceClient.GetUserAsync(token);
        if (result.IsSuccess)
        {
            return ClientErrors.NotFound;
        }
        var getUserResult = new GetUserResult
        {
            UserId = result.Value.UserId,
            Email = result.Value.Email,
            Username = result.Value.Username,
            Role = result.Value.Role
        };
        return getUserResult;
    }

    public async Task<Result> LogoutAsync(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        context.Session.Clear();
        context.Request.Cookies.Select(x => x.Key).ToList().ForEach(x => {
            context.Response.Cookies.Append(x,string.Empty,new CookieOptions {
                Expires = DateTime.Now.AddDays(-1)
            });
        });
        return Result.Success();
    }
}