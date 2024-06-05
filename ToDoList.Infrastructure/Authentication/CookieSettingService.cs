using Microsoft.AspNetCore.Http;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Authentication.Tokens;
using ToDoList.Infrastructure.Extension;

namespace ToDoList.Infrastructure.Authentication
{
    public class CookieSettingService
    {
        private readonly TokenService _tokenService;
        private readonly RefreshTokenService _refreshTokenService;

        public CookieSettingService(TokenService tokenService, RefreshTokenService refreshTokenService)
        {
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        public void SetTokens(HttpContext httpContext, User user)
        {
            var token = _tokenService.GenerateTokenJWT(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            _refreshTokenService.UpsertRefreshToken(refreshToken);

            httpContext.AppendToken(token);
            httpContext.AppendRefreshToken(refreshToken.Token);
        }

        public void RemoveTokens(HttpContext httpContext, string refreshToken)
        {
            _refreshTokenService.Remove(refreshToken, (Guid)_tokenService.UserId);
            httpContext.RemoveToken();
            httpContext.RemoveRefreshToken();
            _tokenService.UserId = null;
        }
    }
}
