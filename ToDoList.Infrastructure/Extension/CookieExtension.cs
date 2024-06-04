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
            Expires = DateTime.UtcNow.AddHours(TokensConst.GetExpiresRefreshToken.Hour),
        };

        public static void AppendRefreshToken(this HttpContext httpContext, string token)
            => httpContext.Response.Cookies.Append(TokensConst.GetRefreshTokenKey, token, _optionRefreshToken);

        public static void AppendToken(this HttpContext httpContext, string token)
            => httpContext.Response.Cookies.Append(TokensConst.GetTokenKey, token, _optionToken);

        public static void RemoveAllTokens(this HttpContext httpContext)
        {
            RemoveToken(httpContext);
            RemoveRefreshToken(httpContext);
        }

        public static void RemoveToken(this HttpContext httpContext)
            => httpContext.Response.Cookies.Delete(TokensConst.GetTokenKey);

        public static void RemoveRefreshToken(this HttpContext httpContext)
            => httpContext.Response.Cookies.Delete(TokensConst.GetRefreshTokenKey);
    }
}
