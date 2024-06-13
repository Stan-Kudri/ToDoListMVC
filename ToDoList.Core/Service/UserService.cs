using ToDoList.Core.DBContext;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Models.Users.PersonalData;
using ToDoListMVC.Models;

namespace ToDoList.Core.Service
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext appDbContext) => _dbContext = appDbContext;

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("The received parameters are not correct.");
            }

            if (_dbContext.Users.Any(e => e.Username == user.Username))
            {
                throw new ArgumentException("This username already exists.");
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdatePersonalData(UserPersonalDataModel userPersonalDate)
        {
            var user = _dbContext.Users.FirstOrDefault(e => e.Id == userPersonalDate.UserId);

            if (user == null || userPersonalDate.Equals(user))
            {
                return;
            }

            user.BirthDate = userPersonalDate.BirthDate;
            user.FirstName = userPersonalDate.FirstName;
            user.LastName = userPersonalDate.LastName;
            user.Country = userPersonalDate.Country;
            user.Gender = userPersonalDate.Gender;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public bool IsFreeUsername(string username) =>
            _dbContext.Users.FirstOrDefault(e => e.Username == username) == null;

        public bool TryGetUserData(UserModel userModel, out User? user)
        {
            var userSelect = _dbContext.Users.FirstOrDefault(e => e.Username == userModel.Username);

            if (userSelect != null && userSelect.IsVerificationPassword(userModel.Password))
            {
                user = userSelect;
                return true;
            }

            user = null;
            return false;
        }

        public User? GetUser(Guid? userId) => _dbContext.Users.FirstOrDefault(e => e.Id == userId);

        public UserPersonalDataModel? GetUserPersonalDate(Guid? userId) => GetUser(userId)?.GetPersonalDate();
    }
}
