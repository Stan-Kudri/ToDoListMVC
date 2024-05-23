using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Core.DBContext;

namespace ToDoList.Migrations.Extension
{
    public static class ApplyMigrationExtension
    {
        public static void Migrate(this AppDbContext dbContext)
        {
            var migrate = dbContext.Database.GetInfrastructure().GetService<IMigrator>()
                ?? throw new InvalidOperationException("Unable to found migrator service.");

            foreach (var migrationName in dbContext.Database.GetPendingMigrations())
            {
                migrate.Migrate(migrationName);
            }
        }
    }
}
