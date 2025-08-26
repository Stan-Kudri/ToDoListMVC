using Microsoft.AspNetCore.Http;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Extension;

namespace ToDoList.Infrastructure.Authentication
{
    public class CookieSettingService(TokenService tokenService, RefreshTokenService refreshTokenService)
        : ICookieSettingService
    {
        public void SetTokens(HttpContext httpContext, User user)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(user);

            var token = tokenService.GenerateTokenJWT(user);
            var refreshToken = tokenService.GenerateRefreshToken(user.Id);

            refreshTokenService.Upsert(refreshToken);

            httpContext.AppendToken(token);
            httpContext.AppendRefreshToken(refreshToken.Token);
        }

        public void RemoveTokens(HttpContext httpContext, string refreshToken)
        {
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentException.ThrowIfNullOrEmpty(refreshToken);

            if (tokenService.UserId.HasValue)
            {
                refreshTokenService.Remove(refreshToken, tokenService.UserId.Value);
            }

            httpContext.RemoveToken();
            httpContext.RemoveRefreshToken();
            tokenService.UserId = null;
        }
    }
}
