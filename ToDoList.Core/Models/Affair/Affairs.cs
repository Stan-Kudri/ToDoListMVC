using System.ComponentModel.DataAnnotations;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.Models.Affair
{
    public class Affairs : Entity
    {
        public Affairs()
        {
        }

        public Affairs(string description, DateTime dateCreate, bool isCaseCompletion, DateTime? dateCompletion, Guid userId)
        {
            Id = Guid.NewGuid();
            Description = description;
            DateCreate = dateCreate;
            IsCaseCompletion = isCaseCompletion;
            DateCompletion = dateCompletion;
            UserId = userId;
        }

        [Required(ErrorMessage = "Please enter description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Error in creation date.")]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        [Required]
        public bool IsCaseCompletion { get; set; } = false;

        public DateTime? DateCompletion { get; set; } = null;

        public User User { get; set; }

        public Guid UserId { get; set; }

        public bool Equals(Affairs? task)
        {
            return task == null
                ? false
                : task.Description == Description
                   && task.IsCaseCompletion == IsCaseCompletion
                   && task.DateCompletion == DateCompletion
                   && task.DateCreate == DateCreate
                   && task.Description == Description
                   && task.UserId == UserId;
        }

        public override bool Equals(object? obj)
            => Equals(obj as Affairs);

        public override int GetHashCode()
            => HashCode.Combine(Id, Description, DateCreate, DateCompletion);
    }
}
