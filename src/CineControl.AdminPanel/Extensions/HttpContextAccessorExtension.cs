namespace CineControl.AdminPanel.Extensions;

public static class HttpContextAccessorExtension
{
    public static string GetTokenValue(this IHttpContextAccessor httpContextAccessor)
        => httpContextAccessor.HttpContext.Request.Cookies["Token"].ToString();
}
