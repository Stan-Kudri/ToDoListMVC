using ToDoList.Core.DBContext;
using ToDoList.Core.Models;
using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.Repository
{
    public class ToDoItemsService(AppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
    {
        private IQueryable<ToDoItems> _getUserTaskQueriably
            => dbContext.ToDoItems.Where(e => e.UserId == currentUserAccessor.UserId);

        public List<ToDoItems> GetComplitedTasks
            => _getUserTaskQueriably.Where(e => e.IsCaseCompletion).ToList();

        public List<ToDoItems> GetNotCompliteTask
            => _getUserTaskQueriably.Where(e => !e.IsCaseCompletion).ToList();

        public void Add(ToDoItemsModel toDoItemModel)
        {
            if (toDoItemModel == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(toDoItemModel));
            }

            if (currentUserAccessor.UserId == null)
            {
                throw new Exception("User authorization error.");
            }

            if (string.IsNullOrEmpty(toDoItemModel.Description))
            {
                return;
            }

            var item = new ToDoItems(
                                    toDoItemModel.Description,
                                    DateTime.Now,
                                    toDoItemModel.IsCaseCompletion,
                                    toDoItemModel.IsCaseCompletion == true ? DateTime.Now : null,
                                    (Guid)currentUserAccessor.UserId);

            dbContext.ToDoItems.Add(item);
            dbContext.SaveChanges();
        }

        public void Update(ToDoItems? item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(item));
            }

            var oldItem = dbContext.ToDoItems.FirstOrDefault(e => e.Id == item.Id);

            if (oldItem == null || oldItem.Equals(item))
            {
                return;
            }

            oldItem.DateCreate = item.DateCreate;
            oldItem.IsCaseCompletion = item.IsCaseCompletion;
            oldItem.DateCompletion = item.DateCompletion;
            oldItem.Description = item.Description;

            dbContext.SaveChanges();
        }

        public void Remove(Guid? id)
        {
            var item = dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            dbContext.ToDoItems.Remove(item);
            dbContext.SaveChanges();
        }

        public void MarkCompleted(Guid? id)
        {
            var item = dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            if (item.IsCaseCompletion)
            {
                item.IsCaseCompletion = false;
                item.DateCompletion = null;
            }
            else
            {
                item.IsCaseCompletion = true;
                item.DateCompletion = DateTime.Now;
            }

            dbContext.SaveChanges();
        }

        public void UpdateDescription(Guid? id, string description)
        {
            var item = dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

            if (item == null || item.Description == description)
            {
                return;
            }

            item.Description = description;
            dbContext.SaveChanges();
        }

        public bool TrySearchItem(Guid? id, out ToDoItems? item)
        {
            item = dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);
            return item != null;
        }
    }
}
