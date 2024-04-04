namespace ToDoList.Models
{
    public record class AffairsModel(string description, bool isCaseCompletion, Guid userId);
}
