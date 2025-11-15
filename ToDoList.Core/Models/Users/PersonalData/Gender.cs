using Ardalis.SmartEnum;

namespace ToDoList.Core.Models.Users.PersonalData
{
    public class Gender : SmartEnum<Gender>
    {
        public static Gender Unknown = new("Not indicate", 0);
        public static Gender Male = new("Male", 1);
        public static Gender Female = new("Female", 2);

        public Gender(string name, int value) : base(name, value)
        {
        }

        public override string ToString() => Name;
    }
}
