using Microsoft.EntityFrameworkCore;
using ToDoList.Core.DBContext;
using ToDoList.Migrations.Extension;

namespace ToDoList.Migrations
{
    public class DbContextFactory
    {
        public string _connectionName;

        public DbContextFactory(string connectionName) => _connectionName = connectionName;

        public AppDbContext Create()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite($"Data Source={_connectionName}.db", x =>
            {
                x.MigrationsAssembly(typeof(DbContextFactoryMigrate).Assembly.FullName);
            });

            var dbContext = new AppDbContext(builder.Options);
            dbContext.Migrate();

            return dbContext;
        }
    }
}
