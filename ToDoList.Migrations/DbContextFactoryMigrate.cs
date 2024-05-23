using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoList.Core.DBContext;

namespace ToDoList.Migrations
{
    public class DbContextFactoryMigrate : IDesignTimeDbContextFactory<AppDbContext>
    {
        private const string ConnectionName = "DbConnection";

        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder()
                                .UseSqlite(
                                    $"Data Source={ConnectionName}.db",
                                    b => b.MigrationsAssembly(typeof(DbContextFactoryMigrate).Assembly.FullName));
            var dbContext = new AppDbContext(builder.Options);
            return dbContext;
        }
    }
}
