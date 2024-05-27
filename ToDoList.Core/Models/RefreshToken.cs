using ToDoList.Core.Authentication;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Models
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public User? User { get; set; } = null;

        public DateTime? Expires { get; set; } = null;

        public DateTime Create { get; set; }

        public bool Expired => DateTime.UtcNow > Expires;

        public bool ShouldUppdate => !Expired && DateTime.UtcNow >= Create.Add(LoginConst.GetUpdateTimeRefreshToken);
    }
}
