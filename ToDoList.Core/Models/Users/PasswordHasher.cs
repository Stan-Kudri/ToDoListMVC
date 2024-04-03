namespace ToDoList.Core.Models.Users
{
    public class PasswordHasher
    {
        public string Generate(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verification(string password, string passwordHash)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}
