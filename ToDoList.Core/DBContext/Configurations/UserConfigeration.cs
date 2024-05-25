using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Extension;
using ToDoList.Core.Models.Users;

namespace ToDoList.Core.DBContext.Configurations
{
    public sealed class UserConfigeration : EntityConfigurationBase<User>
    {
        protected override void ConfigureCore(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasIndex(e => e.Username).IsUnique();
            builder.Property(e => e.Username).IsRequired().HasColumnName("username").HasMaxLength(128);
            builder.Property(e => e.PasswordHash).IsRequired().HasColumnName("passwordHash").HasMaxLength(128);
            builder.Property(e => e.UserRole).IsRequired().HasColumnName("role").HasDefaultValue(UserRole.User).SmartEnumConversion();
            builder.HasMany(e => e.RefreshTokens).WithOne(e => e.User).HasForeignKey(p => p.UserId);
        }
    }
}
