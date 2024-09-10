using Microsoft.AspNetCore.Http;
using ToDoList.Core.Service;
using ToDoList.Infrastructure.Extension;

namespace ToDoList.Infrastructure.Authentication.Tokens
{
    public class TokenValidator
    {
        private readonly TokenService _tokenService;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly UserService _userService;

        private HttpContext _httpContext;
        private string _acsessToken;
        private string _refreshToken;

        public TokenValidator(TokenService tokenService, RefreshTokenService refreshTokenService, UserService userService)
        {
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
        }

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

            _tokenService.SetAcsessToken(_acsessToken);
            var userId = _tokenService.UserId;
            var refreshToken = _refreshTokenService.GetRefreshToken(_refreshToken, (Guid)userId);

            if (refreshToken != null && _refreshTokenService.IsExistRefreshToken(refreshToken) && !refreshToken.Expired)
            {
                return true;
            }

            _httpContext.RemoveAllTokens();
            return false;
        }

        public void UpdateTokens()
        {
            var user = _userService.GetUser(_tokenService.UserId);

            if (user == null)
            {
                throw new Exception("Database request error.");
            }

            if (_tokenService.ShouldUppdateAcsessToken(_acsessToken))
            {
                var newAcsessToken = _tokenService.GenerateTokenJWT(user);
                _tokenService.SetAcsessToken(newAcsessToken);
                _httpContext.AppendToken(newAcsessToken);
            }

            var refreshToken = _refreshTokenService.GetRefreshToken(_refreshToken, user.Id);

            if (refreshToken != null && refreshToken.ShouldUppdate)
            {
                _refreshTokenService.Update(refreshToken, out var UpdatedToken);
                _httpContext.AppendRefreshToken(UpdatedToken.Token);
            }
        }

        private bool IsValidAcsessToken() => _tokenService.ValidAcsessToken(_acsessToken);
    }
}
