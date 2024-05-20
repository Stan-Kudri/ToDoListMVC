namespace ToDoList.Core.Models
{
    public class AccessToken
    {
        public string? Token { get; set; }

        public RefreshToken? RefreshToken { get; set; }
    }
}
