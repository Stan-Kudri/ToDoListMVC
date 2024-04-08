using ToDoList.Core.Models.Users;

namespace ToDoListMVC.Models
{
    public class UserModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;

        //[Required(ErrorMessage = "Please enter your username")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{6,30}$", ErrorMessage = "The username is not in the correct format.")]
        public string Username
        {
            get => _username;
            set => _username = value;
        }

        //[Required(ErrorMessage = "Please enter password")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,30}$", ErrorMessage = "The password is not in the correct format.")]
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public User ToUser() => new User(_username, _password);
    }
}
