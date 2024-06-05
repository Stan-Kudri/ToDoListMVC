using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.Models.Users
{
    public class User : Entity
    {
        private static UserValidator _userValidator = new UserValidator();
        private static PasswordHasher _passwordHasher = new PasswordHasher();

        public User()
        {
        }

        public User(string username, string password)
            : this(Guid.NewGuid(), username, password)
        {
        }

        public User(Guid id, string username, string password)
        {
            Id = id;

            if (!_userValidator.ValidatePassword(password, out var messageValidPass))
            {
                throw new ArgumentException(messageValidPass, nameof(password));
            }

            if (!_userValidator.ValidateUsername(username, out var messageValidUsername))
            {
                throw new ArgumentException(messageValidUsername, nameof(username));
            }

            Username = username;
            PasswordHash = _passwordHasher.Generate(password);
        }

        public string Username { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public UserRole UserRole { get; set; } = UserRole.User;

        public List<ToDoItems>? ToDoItems { get; set; } = null;

        public List<RefreshToken> RefreshTokens { get; set; } = null;

        public bool IsVerificationPassword(string password)
            => _passwordHasher.Verification(password, PasswordHash);
    }
}
