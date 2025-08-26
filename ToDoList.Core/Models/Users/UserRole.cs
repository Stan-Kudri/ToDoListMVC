using Ardalis.SmartEnum;

namespace ToDoList.Core.Models.Users
{
    public class UserRole(string name, int value) : SmartEnum<UserRole>(name, value)
    {
        public static UserRole User = new UserRole("User", 0);

        public static UserRole Admin = new UserRole("Admin", 1);

        public override string ToString() => Name;
    }
}
