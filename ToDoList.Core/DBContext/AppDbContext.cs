using Microsoft.EntityFrameworkCore;
using ToDoList.Core.DBContext.Configurations;
using ToDoList.Core.Models;
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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfigeration).Assembly);
        /*
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
            configurationUser.HasMany(e => e.RefreshTokens).WithOne(e => e.User).HasForeignKey(p => p.UserId);

            var configurationAffairs = modelBuilder.Entity<Affairs>();
            configurationAffairs.ToTable("affairs");
            configurationAffairs.HasKey(e => e.Id);
            configurationAffairs.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            configurationAffairs.Property(e => e.Description).IsRequired().HasMaxLength(128).HasColumnName("description");
            configurationAffairs.Property(e => e.DateCreate).IsRequired().HasDefaultValue(DateTime.Now).HasColumnName("datecreate");
            configurationAffairs.Property(e => e.DateCompletion).HasDefaultValue(null).HasColumnName("datacompletion");
            configurationAffairs.Property(e => e.IsCaseCompletion).IsRequired().HasDefaultValue(false).HasColumnName("iscompletion").HasColumnType("BOOLEAN");
            configurationAffairs.HasOne(e => e.User).WithMany(e => e.Affairs).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

            var configurationRefreshToken = modelBuilder.Entity<RefreshToken>();
            configurationRefreshToken.ToTable("refreshtoken");
            configurationRefreshToken.HasKey(e => e.UserId);
            configurationRefreshToken.Property(e => e.UserId).IsRequired().HasColumnName("userid");
            configurationRefreshToken.Property(e => e.Token).HasDefaultValue(null).HasColumnName("refreshtoken");
            configurationRefreshToken.Property(e => e.Expires).IsRequired().HasColumnName("datetimeexpires").HasDefaultValue(LoginConst.GetExpiresRefreshToken).HasColumnType("DATETIME");
            configurationRefreshToken.Property(e => e.Create).IsRequired().HasColumnName("datetimecreate").HasDefaultValue(DateTime.UtcNow).HasColumnType("DATETIME");
            configurationRefreshToken.HasOne(e => e.User).WithMany(e => e.RefreshTokens).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            configurationRefreshToken.Ignore(e => e.Expired);
            configurationRefreshToken.Ignore(e => e.ShouldUppdate);
        }
        */
    }
}
