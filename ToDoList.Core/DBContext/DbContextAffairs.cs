using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;

namespace ToDoList.Core.DBContext
{
    public class DbContextAffairs : DbContext
    {
        public DbContextAffairs(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<Affairs> Affairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Affairs>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
                builder.Property(e => e.Description).IsRequired().HasMaxLength(128).HasColumnName("casename");
                builder.Property(e => e.DateCreate).IsRequired().HasDefaultValue(DateTime.Now).HasColumnName("datecreate");
                builder.Property(e => e.DateCompletion).HasDefaultValue(null).HasColumnName("datacompletion");
                builder.Property(e => e.IsCaseCompletion).IsRequired().HasDefaultValue(false).HasColumnName("iscomletion");
            });
        }
    }
}
