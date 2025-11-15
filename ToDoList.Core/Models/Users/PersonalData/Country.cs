using Ardalis.SmartEnum;

namespace ToDoList.Core.Models.Users.PersonalData
{
    public class Country : SmartEnum<Country>
    {
        public static Country Unknown = new("Not indicate", 0);
        public static Country Australia = new("Australia", 1);
        public static Country Armenia = new("Armenia", 2);
        public static Country Belarus = new("Belarus", 3);
        public static Country Brazil = new("Brazil", 4);
        public static Country UK = new("United Kingdom", 5);
        public static Country Hungary = new("Hungary", 6);
        public static Country Greece = new("Greece", 7);
        public static Country Georgia = new("Georgia", 8);
        public static Country Denmark = new("Denmark", 9);
        public static Country Egypt = new("Egypt", 10);
        public static Country Israel = new("Israel", 11);
        public static Country India = new("India", 12);
        public static Country Italy = new("Italy", 13);
        public static Country Kenya = new("Kenya", 14);
        public static Country China = new("China", 15);
        public static Country Latvia = new("Latvia", 16);
        public static Country Lithuania = new("Lithuania", 17);
        public static Country Mexico = new("Mexico", 18);
        public static Country NewZealand = new("New Zealand", 19);
        public static Country Norway = new("Norway", 20);
        public static Country Poland = new("Poland", 21);
        public static Country Russia = new("Russia", 22);
        public static Country USA = new("USA", 23);
        public static Country Tajikistan = new("Tajikistan", 24);
        public static Country Thailand = new("Thailand", 25);
        public static Country Uzbekistan = new("Uzbekistan", 26);
        public static Country Ukraine = new("Ukraine", 27);
        public static Country France = new("France", 28);
        public static Country Switzerland = new("Switzerland", 29);
        public static Country Ethiopia = new("Ethiopia", 30);
        public static Country Jamaica = new("Jamaica", 31);
        public static Country Japan = new("Japan", 32);

        public Country(string name, int value) : base(name, value)
        {
        }

        public override string ToString() => Name;
    }
}
