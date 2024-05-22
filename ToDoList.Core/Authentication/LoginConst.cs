namespace ToDoList.Core.Authentication
{
    public static class LoginConst
    {
        public static string GetTokenKey => "JWTBearer";

        public static string GetRefreshTokenKey => "RefreshToken";

        public static DateTime GetExpiresRefreshToken => DateTime.UtcNow.AddMinutes(2);//=> DateTime.UtcNow.AddHours(24);

        public static DateTime GetDateCreateRefreshToken => DateTime.UtcNow;

        public static TimeSpan GetUpdateTimeRefreshToken => TimeSpan.FromSeconds(30);//TimeSpan.FromHours(2);
    }
}
