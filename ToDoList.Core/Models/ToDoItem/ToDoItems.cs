using System.ComponentModel.DataAnnotations;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Models.ToDoItem
{
    public class ToDoItems : Entity
    {
        public ToDoItems()
        {
        }

        public ToDoItems(string description, DateTime dateCreate, bool isCaseCompletion, DateTime? dateCompletion, Guid userId)
        {
            Id = Guid.NewGuid();
            Description = description;
            CreateDate = dateCreate;
            IsCaseCompletion = isCaseCompletion;
            CompletDate = dateCompletion;
            UserId = userId;
        }

        [Required(ErrorMessage = "Please enter description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Error in creation date.")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public bool IsCaseCompletion { get; set; } = false;

        public DateTime? CompletDate { get; set; } = null;

        public User User { get; set; }

        public Guid UserId { get; set; }

        public bool Equals(ToDoItems? task) => task != null
                                               && task.Description == Description
                                               && task.IsCaseCompletion == IsCaseCompletion
                                               && task.CompletDate == CompletDate
                                               && task.CreateDate == CreateDate
                                               && task.Description == Description
                                               && task.UserId == UserId;

        public override bool Equals(object? obj) => Equals(obj as ToDoItems);

        public override int GetHashCode()
            => HashCode.Combine(Id, Description, CreateDate, CompletDate);
    }
}
