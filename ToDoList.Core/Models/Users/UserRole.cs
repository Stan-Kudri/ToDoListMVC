using Ardalis.SmartEnum;

namespace ToDoList.Core.Models.Users
{
    public class UserRole : SmartEnum<UserRole>
    {
        public static UserRole User = new UserRole("User", 0);

        public static UserRole Admin = new UserRole("Admin", 1);

        public UserRole(string name, int value)
            : base(name, value)
        {
        }

        public override string ToString() => Name;
    }
}
