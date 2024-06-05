namespace ToDoList.Core.Authentication
{
    public static class TokensConst
    {
        public static string GetTokenKey => "JWTBearer";

        public static string GetRefreshTokenKey => "RefreshToken";

        public static TimeSpan GetUpdateTimeToken => TimeSpan.FromMinutes(30);

        public static DateTime GetDateCreateRefreshToken => DateTime.UtcNow;

        public static DateTime GetExpiresRefreshToken => DateTime.UtcNow.AddHours(24);

        public static TimeSpan GetUpdateTimeRefreshToken => TimeSpan.FromMinutes(60);
    }
}
