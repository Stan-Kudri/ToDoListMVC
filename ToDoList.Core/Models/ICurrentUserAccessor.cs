namespace ToDoList.Core.Models
{
    public interface ICurrentUserAccessor
    {
        Guid? UserId { get; set; }
    }
}
