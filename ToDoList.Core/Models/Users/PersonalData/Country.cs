using Ardalis.SmartEnum;

namespace ToDoList.Core.Models.Users.PersonalData
{
    public class Country : SmartEnum<Country>
    {
        public static Country Unknown = new Country("Not indicate", 0);
        public static Country Australia = new Country("Australia", 1);
        public static Country Armenia = new Country("Armenia", 2);
        public static Country Belarus = new Country("Belarus", 3);
        public static Country Brazil = new Country("Brazil", 4);
        public static Country UK = new Country("United Kingdom", 5);
        public static Country Hungary = new Country("Hungary", 6);
        public static Country Greece = new Country("Greece", 7);
        public static Country Georgia = new Country("Georgia", 8);
        public static Country Denmark = new Country("Denmark", 9);
        public static Country Egypt = new Country("Egypt", 10);
        public static Country Israel = new Country("Israel", 11);
        public static Country India = new Country("India", 12);
        public static Country Italy = new Country("Italy", 13);
        public static Country Kenya = new Country("Kenya", 14);
        public static Country China = new Country("China", 15);
        public static Country Latvia = new Country("Latvia", 16);
        public static Country Lithuania = new Country("Lithuania", 17);
        public static Country Mexico = new Country("Mexico", 18);
        public static Country NewZealand = new Country("New Zealand", 19);
        public static Country Norway = new Country("Norway", 20);
        public static Country Poland = new Country("Poland", 21);
        public static Country Russia = new Country("Russia", 22);
        public static Country USA = new Country("USA", 23);
        public static Country Tajikistan = new Country("Tajikistan", 24);
        public static Country Thailand = new Country("Thailand", 25);
        public static Country Uzbekistan = new Country("Uzbekistan", 26);
        public static Country Ukraine = new Country("Ukraine", 27);
        public static Country France = new Country("France", 28);
        public static Country Switzerland = new Country("Switzerland", 29);
        public static Country Ethiopia = new Country("Ethiopia", 30);
        public static Country Jamaica = new Country("Jamaica", 31);
        public static Country Japan = new Country("Japan", 32);

        public Country(string name, int value) : base(name, value)
        {
        }

        public override string ToString() => Name;
    }
}
