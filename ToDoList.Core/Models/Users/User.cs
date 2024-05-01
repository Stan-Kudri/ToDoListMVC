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

            if (!_userValidator.ValidFormatPassword(password, out var messageValidPass))
            {
                throw new ArgumentException(messageValidPass, nameof(password));
            }

            if (!_userValidator.ValidFormatUsername(username, out var messageValidUsername))
            {
                throw new ArgumentException(messageValidUsername, nameof(username));
            }

            Username = username;
            PasswordHash = _passwordHasher.Generate(password);
        }

        public string Username { get; private set; } = string.Empty;

        public string PasswordHash { get; private set; } = string.Empty;

        public UserRole UserRole { get; set; } = UserRole.User;

        public List<Affairs>? Affairs { get; set; } = null;

        public bool IsVerificationPassword(string password)
            => _passwordHasher.Verification(password, PasswordHash);
    }
}
