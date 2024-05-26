namespace ToDoList.Core.Models.ToDoItem
{
    public record class ToDoItemsModel(string Description, bool IsCaseCompletion, Guid UserId);
}
