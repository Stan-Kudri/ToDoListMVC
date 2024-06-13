namespace ToDoList.Core.Models.Users.PersonalData
{
    public class UserPersonalDataModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; } = Gender.Unknown;

        public Country Country { get; set; } = Country.Unknown;

        public DateTime? BirthDate { get; set; } = null;

        public UserPersonalDataModel()
        {
        }

        public UserPersonalDataModel(Guid userId, string firstName, string lastName, Gender gender, Country country, DateTime? birthDate)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Country = country;
            BirthDate = birthDate;
        }

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
