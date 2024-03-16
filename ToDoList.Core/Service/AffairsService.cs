using ToDoList.Core.DBContext;
using ToDoList.Models;

namespace ToDoList.Core.Repository
{
    public class AffairsService
    {
        private readonly DbContextAffairs _dbContext;

        public AffairsService(DbContextAffairs dbContext)
            => _dbContext = dbContext;

        public void Add(Affairs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(item));
            }

            _dbContext.Affairs.Add(item);
            _dbContext.SaveChanges();
        }

        public void Update(Affairs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Case item has a value of zero.", nameof(item));
            }

            var oldItem = _dbContext.Affairs.FirstOrDefault(e => e.Id == item.Id);

            if (oldItem == null || !oldItem.Equals(item))
            {
                return;
            }

            oldItem.DateCreate = item.DateCreate;
            oldItem.IsCaseCompletion = item.IsCaseCompletion;
            oldItem.DateCompletion = item.DateCompletion;
            oldItem.Description = item.Description;

            _dbContext.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var item = _dbContext.Affairs.FirstOrDefault(e => e.Id == id);

            if (item == null)
            {
                return;
            }

            _dbContext.Affairs.Remove(item);
            _dbContext.SaveChanges();
        }

        public List<Affairs> GetAll()
        {
            return _dbContext.Affairs.Count() > 0
                    ? _dbContext.Affairs.ToList()
                    : new List<Affairs>();
        }
    }
}
