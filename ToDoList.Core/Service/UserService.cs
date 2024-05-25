using ToDoList.Core.DBContext;
using ToDoList.Core.Models.Users;
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
                throw new ArgumentException("This username exists.");
            }

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public bool IsFreeUsername(string username) =>
            _dbContext.Users.FirstOrDefault(e => e.Username == username) == null;

        public bool IsUserModelData(UserModel userModel, out User? user)
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
    }
}
