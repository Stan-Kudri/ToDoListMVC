using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Extension;
using ToDoList.Core.Models.Affair;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Affairs> Affairs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurationUser = modelBuilder.Entity<User>();
            configurationUser.ToTable("user");
            configurationUser.HasKey(e => e.Id);
            configurationUser.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            configurationUser.HasIndex(e => e.Username).IsUnique();
            configurationUser.Property(e => e.Username).IsRequired().HasColumnName("username").HasMaxLength(128);
            configurationUser.Property(e => e.PasswordHash).IsRequired().HasColumnName("passwordHash").HasMaxLength(128);
            configurationUser.Property(e => e.UserRole).IsRequired().HasColumnName("role").HasDefaultValue(UserRole.User).SmartEnumConversion();

            var configurationAffairs = modelBuilder.Entity<Affairs>();
            configurationAffairs.ToTable("affairs");
            configurationAffairs.HasKey(e => e.Id);
            configurationAffairs.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            configurationAffairs.Property(e => e.Description).IsRequired().HasMaxLength(128).HasColumnName("description");
            configurationAffairs.Property(e => e.DateCreate).IsRequired().HasDefaultValue(DateTime.Now).HasColumnName("datecreate");
            configurationAffairs.Property(e => e.DateCompletion).HasDefaultValue(null).HasColumnName("datacompletion");
            configurationAffairs.Property(e => e.IsCaseCompletion).IsRequired().HasDefaultValue(false).HasColumnName("iscompletion").HasColumnType("BOOLEAN");
            configurationAffairs.HasOne(e => e.User).WithMany(e => e.Affairs).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
