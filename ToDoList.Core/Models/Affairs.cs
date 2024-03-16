using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Affairs : Entity
    {
        public Affairs(string description, DateTime dateCreate, bool isCaseCompletion, DateTime? dateCompletion)
        {
            Id = Guid.NewGuid();
            Description = description;
            DateCreate = dateCreate;
            IsCaseCompletion = isCaseCompletion;
            DateCompletion = dateCompletion;
        }

        [Required(ErrorMessage = "Please enter description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Error in creation date.")]
        public DateTime DateCreate { get; set; }

        [Required]
        public bool IsCaseCompletion { get; set; } = false;

        public DateTime? DateCompletion { get; set; } = null;

        public bool Equals(Affairs? task)
        {
            if (task == null)
            {
                return false;
            }

            return task.Description == Description
                   && task.IsCaseCompletion == IsCaseCompletion
                   && task.DateCompletion == DateCompletion
                   && task.DateCreate == DateCreate
                   && task.Description == Description;
        }

        public override bool Equals(object? obj)
            => Equals(obj as Affairs);

        public override int GetHashCode()
            => HashCode.Combine(Id, Description, DateCreate, DateCompletion);

    }
}
