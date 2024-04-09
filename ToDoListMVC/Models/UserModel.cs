using ToDoList.Core.Models.Users;

namespace ToDoListMVC.Models
{
    public class UserModel
    {
        private string _username = string.Empty;
        private string _password = string.Empty;

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public User ToUser() => new User(_username, _password);
    }
}
