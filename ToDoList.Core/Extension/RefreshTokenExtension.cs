﻿using ToDoList.Core.Authentication;
using ToDoList.Core.Models;
using ToDoList.Core.Service;

namespace ToDoList.Core.Extension
{
    public static class RefreshTokenExtension
    {
        public static void UppdataRefreshToken(this RefreshTokenService refreshTokenService, RefreshToken refreshToken)
        {
            if (refreshTokenService.IsUserIdExist(refreshToken.UserId))
            {
                refreshTokenService.Uppdata(refreshToken);
            }
            else
            {
                refreshTokenService.Add(refreshToken);
            }
        }

        public static bool IsExpiredRefreshToken(this RefreshToken refreshToken)
            => DateTime.UtcNow.Add(LoginConst.GetUpdateTimeRefreshToken) <= refreshToken.Expires;

        public static bool IsActiveRefreshToken(this RefreshToken refreshToken)
            => DateTime.UtcNow <= refreshToken.Expires;
    }
}