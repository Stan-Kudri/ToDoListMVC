using ToDoList.Core.Models.ToDoItem;
using ToDoList.Core.Models.Users.PersonalData;

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

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.Unknown;

        public Country Country { get; set; } = Country.Unknown;

        public DateTime? BirthDate { get; set; } = null;

        public List<ToDoItems>? ToDoItems { get; set; } = null;

        public List<RefreshToken> RefreshTokens { get; set; } = null;

        public UserPersonalDataModel GetPersonalDate()
            => new UserPersonalDataModel(Id, FirstName, LastName, Gender, Country, BirthDate);

        public bool IsVerificationPassword(string password)
            => _passwordHasher.Verification(password, PasswordHash);

        public override bool Equals(object? obj)
            => Equals(obj as User);

        public bool Equals(User? user)
            => user == null
                ? false
                : user.FirstName == FirstName
                  && user.LastName == LastName
                  && user.BirthDate == BirthDate
                  && user.Country == Country
                  && user.Gender == Gender;
    }
}
