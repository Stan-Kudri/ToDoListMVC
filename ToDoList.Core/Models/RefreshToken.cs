using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public User? User { get; set; } = null;

        public DateTime? Expires { get; set; } = null;

        public DateTime Create { get; set; }
    }
}
