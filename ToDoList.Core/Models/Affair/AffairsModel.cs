namespace ToDoList.Core.Models.Affair
{
    public record class AffairsModel(string Description, bool IsCaseCompletion, Guid UserId);
}
