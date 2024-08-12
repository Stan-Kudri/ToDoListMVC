using ToDoList.Core.DBContext;
using ToDoList.Core.Models;
using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.Repository
{
    public class ToDoItemsService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserAccessor _currentUser;

        private IQueryable<ToDoItems> GetUserTaskQueriably
            => _dbContext.ToDoItems.Where(e => e.UserId == _currentUser.UserId);

        public ToDoItemsService(AppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _currentUser = currentUserAccessor;
        }

        public void Add(ToDoItemsModel toDoItemModel)
        {
            if (toDoItemModel == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(toDoItemModel));
            }

            if (_currentUser.UserId == null)
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
                                    (Guid)_currentUser.UserId);

            _dbContext.ToDoItems.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(ToDoItems? item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(item));
            }

            var oldItem = _dbContext.ToDoItems.FirstOrDefault(e => e.Id == item.Id);

            if (oldItem == null || oldItem.Equals(item))
            {
                return;
            }

            oldItem.DateCreate = item.DateCreate;
            oldItem.IsCaseCompletion = item.IsCaseCompletion;
            oldItem.DateCompletion = item.DateCompletion;
            oldItem.Description = item.Description;

            _dbContext.SaveChanges();
        }

        public void Remove(Guid? id)
        {
            var item = _dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            _dbContext.ToDoItems.Remove(item);
            _dbContext.SaveChanges();
        }

        public void MarkCompleted(Guid? id)
        {
            var item = _dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

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

            _dbContext.SaveChanges();
        }

        public void UpdateDescription(Guid? id, string description)
        {
            var item = _dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);

            if (item == null || item.Description == description)
            {
                return;
            }

            item.Description = description;
            _dbContext.SaveChanges();
        }

        public bool TrySearchItem(Guid? id, out ToDoItems? item)
        {
            item = _dbContext.ToDoItems.FirstOrDefault(e => e.Id == id);
            return item != null;
        }

        public List<ToDoItems> GetComplitedTasks()
            => GetUserTaskQueriably.Where(e => e.IsCaseCompletion).ToList();

        public List<ToDoItems> GetNotCompliteTask()
            => GetUserTaskQueriably.Where(e => !e.IsCaseCompletion).ToList();
    }
}
