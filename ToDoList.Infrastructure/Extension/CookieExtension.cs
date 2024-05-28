using Microsoft.AspNetCore.Http;
using ToDoList.Core.Authentication;

namespace ToDoList.Infrastructure.Extension
{
    public static class CookieExtension
    {
        private static readonly CookieOptions _optionToken = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
        };

        private static readonly CookieOptions _optionRefreshToken = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(LoginConst.GetExpiresRefreshToken.Hour),
        };

        public static void AppendRefreshToken(this HttpContext httpContext, string token)
            => Append(httpContext, LoginConst.GetRefreshTokenKey, token, _optionRefreshToken);

        public static void AppendToken(this HttpContext httpContext, string token)
            => Append(httpContext, LoginConst.GetTokenKey, token, _optionToken);

        public static void RemoveAllToken(this HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(LoginConst.GetTokenKey);
            httpContext.Response.Cookies.Delete(LoginConst.GetRefreshTokenKey);
        }

        public static void RemoveToken(this HttpContext httpContext)
            => httpContext.Response.Cookies.Delete(LoginConst.GetTokenKey);

        public static void RemoveRefreshToken(this HttpContext httpContext)
            => httpContext.Response.Cookies.Delete(LoginConst.GetRefreshTokenKey);

        private static void Append(this HttpContext httpContext, string key, string token, CookieOptions cookieOptions)
        {
            httpContext.Response.Cookies.Delete(key);
            httpContext.Response.Cookies.Append(key, token, cookieOptions);
        }
    }
}
