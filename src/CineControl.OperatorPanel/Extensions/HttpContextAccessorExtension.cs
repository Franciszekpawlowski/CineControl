namespace CineControl.OperatorPanel.Extensions;

public static class HttpContextAccessorExtension
{
    public static string GetTokenValue(this IHttpContextAccessor httpContextAccessor)
        => httpContextAccessor.HttpContext.Request.Cookies["TenantId"].ToString();
}
