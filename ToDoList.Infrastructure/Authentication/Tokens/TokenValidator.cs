using Microsoft.AspNetCore.Http;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Extension;

namespace ToDoList.Infrastructure.Authentication.Tokens
{
    public class TokenValidator(TokenService tokenService, RefreshTokenService refreshTokenService, UserService userService)
    {
        private HttpContext _httpContext = null!;
        private string _acsessToken = null!;
        private string _refreshToken = null!;

        public void InitializingParameters(HttpContext httpContext, string acsessToken, string refreshToken)
        {
            _httpContext = httpContext;
            _acsessToken = acsessToken;
            _refreshToken = refreshToken;
        }

        public bool IsValidTokensFromCookies()
        {
            if (!IsValidAcsessToken())
            {
                _httpContext.RemoveAllTokens();
                return false;
            }

            tokenService.SetAcsessToken(_acsessToken);
            var userId = tokenService.UserId ?? throw new ApplicationException("The user with this ID is not authenticated.");
            var refreshToken = refreshTokenService.GetRefreshToken(_refreshToken, userId);

            if (refreshToken != null && refreshTokenService.IsExistRefreshToken(refreshToken) && !refreshToken.Expired)
            {
                return true;
            }

            _httpContext.RemoveAllTokens();
            return false;
        }

        public void UpdateTokens()
        {
            var user = userService.GetUser(tokenService.UserId);

            if (user == null)
            {
                throw new Exception("Database request error.");
            }

            if (tokenService.ShouldUppdateAcsessToken(_acsessToken))
            {
                var newAcsessToken = tokenService.GenerateTokenJWT(user);
                tokenService.SetAcsessToken(newAcsessToken);
                _httpContext.AppendToken(newAcsessToken);
            }

            var refreshToken = refreshTokenService.GetRefreshToken(_refreshToken, user.Id);

            if (refreshToken != null && refreshToken.ShouldUppdate)
            {
                refreshTokenService.Update(refreshToken, out var UpdatedToken);
                _httpContext.AppendRefreshToken(UpdatedToken.Token);
            }
        }

        private bool IsValidAcsessToken() => tokenService.ValidAcsessToken(_acsessToken);
    }
}
