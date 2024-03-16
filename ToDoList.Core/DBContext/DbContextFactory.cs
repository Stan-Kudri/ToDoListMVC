using Microsoft.EntityFrameworkCore;

namespace ToDoList.Core.DBContext
{
    public class DbContextFactory
    {
        private string _connectionName;

        public DbContextFactory(string path) => _connectionName = path;

        public DbContextAffairs Create()
        {
            var builder = new DbContextOptionsBuilder().UseSqlite($"Data Source={_connectionName}.db");
            var appDbContext = new DbContextAffairs(builder.Options);
            appDbContext.Database.EnsureCreated();
            return appDbContext;
        }
    }
}
