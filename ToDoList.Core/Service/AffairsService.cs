using ToDoList.Core.DBContext;
using ToDoList.Core.Models;
using ToDoList.Core.Models.Affair;

namespace ToDoList.Core.Repository
{
    public class AffairsService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserAccessor _currentUser;

        public AffairsService(AppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _currentUser = currentUserAccessor;
        }

        public void Add(AffairsModel affairsModel)
        {
            if (affairsModel == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(affairsModel));
            }

            if (_currentUser.UserId == null)
            {
                throw new Exception("User authorization error.");
            }

            if (string.IsNullOrEmpty(affairsModel.Description))
            {
                return;
            }

            var item = new Affairs(
                                    affairsModel.Description,
                                    DateTime.Now,
                                    affairsModel.IsCaseCompletion,
                                    affairsModel.IsCaseCompletion == true ? DateTime.Now : null,
                                    (Guid)_currentUser.UserId);

            _dbContext.Affairs.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(Affairs? item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(item));
            }

            var oldItem = _dbContext.Affairs.FirstOrDefault(e => e.Id == item.Id);

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
            var item = _dbContext.Affairs.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            _dbContext.Affairs.Remove(item);
            _dbContext.SaveChanges();
        }

        public void MarkCompleted(Guid? id)
        {
            var item = _dbContext.Affairs.FirstOrDefault(e => e.Id == id);

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
            var item = _dbContext.Affairs.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            if (item.Description == description)
            {
                return;
            }

            item.Description = description;
            _dbContext.SaveChanges();
        }

        public bool TrySearchItem(Guid? id, out Affairs item)
        {
            item = _dbContext.Affairs.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return false;
            }

            return true;
        }

        public List<Affairs> GetCompliteTask()
        {
            var query = _dbContext.Affairs.Where(e => e.UserId == _currentUser.UserId).Where(e => e.IsCaseCompletion);

            return query.Count() > 0
                   ? query.ToList()
                   : new List<Affairs>();
        }

        public List<Affairs> GetNotCompliteTask()
        {
            var query = _dbContext.Affairs.Where(e => e.UserId == _currentUser.UserId).Where(e => !e.IsCaseCompletion);

            return query.Count() > 0
                   ? query.ToList()
                   : new List<Affairs>();
        }
    }
}
