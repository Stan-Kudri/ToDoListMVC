namespace ToDoList.Core.Models.Users
{
    public class UserValidator
    {
        public const int MinLengthUsername = 6;
        public const int MinLengthPassword = 8;

        public bool ValidetUsername(string username, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                message = "Please enter your username.";
                return false;
            }

            if (username.Length < MinLengthUsername)
            {
                message = "The username is too short.";
                return false;
            }

            if (!char.IsLetter(username[0]))
            {
                message = "Your username has to start with a letter.";
                return false;
            }

            if (!username.All(char.IsLetterOrDigit))
            {
                message = "Illegal characters in string.";
                return false;
            }

            return true;
        }

        public bool ValidatePassword(string password, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(password))
            {
                message = "Please enter password.";
                return false;
            }

            if (password.Length < MinLengthPassword)
            {
                message = "Your password is too short.";
                return false;
            }

            if (!password.All(char.IsLetterOrDigit))
            {
                message = "Illegal characters in string.";
                return false;
            }

            return true;
        }
    }
}
