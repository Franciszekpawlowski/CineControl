using System.Security.Claims;
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

public class AuthService(IIdentityServiceClient authServiceClient, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly IIdentityServiceClient _authServiceClient = authServiceClient;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<Result> LoginAsync(UserLoginRequest userRequest)
    {
        
        var loginRequestModel = new LoginRequestModel() {
            Username = userRequest.Username,
            Password = userRequest.Password
        };
        var loginResponseModel = await _authServiceClient.LoginAsync(loginRequestModel);
        if (!loginResponseModel.IsSuccess)
        {
            return AuthServiceErrors.AccessUnauthorized();
        }
        _httpContextAccessor.HttpContext.AddCookies(loginResponseModel.Value!);
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

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authenticationProperties
        );

        return Result.Success();
    }

    public async Task<ResultT<GetUserResult>> GetUserAsync()
    {
        string token = _httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
        
        if (string.IsNullOrEmpty(token))
        {
            return AuthServiceErrors.AccessUnauthorized();
        }

        var result = await _authServiceClient.GetUserAsync(token);
        if (!result.IsSuccess)
        {
            return AuthServiceErrors.NotFound();
        }
        return new GetUserResult(result.Value);
    }

    public async Task<Result> LogoutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _httpContextAccessor.HttpContext.Request.Cookies.Select(x => x.Key).ToList().ForEach(x => {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(x,string.Empty,new CookieOptions {
                Expires = DateTime.Now.AddDays(-1)
            });
        });
        return Result.Success();
    }
}