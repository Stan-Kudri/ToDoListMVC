using ToDoList.Core.Authentication;
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
                refreshTokenService.Update(refreshToken);
            }
            else
            {
                refreshTokenService.Add(refreshToken);
            }
        }

        public static bool IsUppdateRefreshToken(this RefreshToken refreshToken)
            => DateTime.UtcNow <= refreshToken.Expires && DateTime.UtcNow >= refreshToken.Create.Add(LoginConst.GetUpdateTimeRefreshToken);

        public static bool IsActiveRefreshToken(this RefreshToken refreshToken)
            => DateTime.UtcNow <= refreshToken.Expires;
    }
}
