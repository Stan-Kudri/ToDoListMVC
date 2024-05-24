﻿using ToDoList.Core.Authentication;
using ToDoList.Core.Extension;
using ToDoList.Core.Service;
using ToDoListMVC.Extension;

namespace ToDoListMVC.Models
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
                return false;
            }

            _tokenService.SetAcsessToken(_acsessToken);
            var userId = _tokenService.UserId;

            if (userId == null)
            {
                return false;
            }

            var refreshToken = _refreshTokenService.GetRefreshToken(_refreshToken, (Guid)userId);

            if (refreshToken != null && _refreshTokenService.IsExistRefreshToken(refreshToken) && refreshToken.IsActiveRefreshToken())
            {
                return true;
            }

            _httpContext.RemoveRefreshToken();
            _httpContext.RemoveToken();
            return false;
        }

        public void UpdateTokens()
        {
            var user = _userService.GetUser(_tokenService.UserId);

            if (user == null)
            {
                throw new Exception("Database request error.");
            }

            if (_tokenService.IsUppdateAcsessToken(_acsessToken))
            {
                var newAcsessToken = _tokenService.GenerateTokenJWT(user);
                _tokenService.SetAcsessToken(newAcsessToken);
                _httpContext.AppendToken(newAcsessToken);
            }

            var refreshToken = _refreshTokenService.GetRefreshToken(_refreshToken, user.Id);

            if (refreshToken != null && refreshToken.IsUppdateRefreshToken())
            {
                var newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);
                _refreshTokenService.Update(newRefreshToken);
                _httpContext.AppendRefreshToken(newRefreshToken.Token);
            }
        }

        private bool IsValidAcsessToken() => _tokenService.ValidAcsessToken(_acsessToken);
    }
}
