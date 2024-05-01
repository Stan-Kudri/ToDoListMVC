using ToDoList.Core.Extension;

namespace ToDoList.Core.Authentication
{
    public static class LoginConst
    {
        public static string GetTokenKey => "JWTBearer";

        public static string Encrypt(this string str)
            => str.EncryptString();

        public static string Decrypt(this string str)
            => str.DecryptString();
    }
}
