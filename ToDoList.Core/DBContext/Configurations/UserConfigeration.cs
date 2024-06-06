using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Extension;
using ToDoList.Core.Models.Users;
using ToDoList.Core.Models.Users.PersonalData;

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
            builder.Property(e => e.FirstName).IsRequired().HasColumnName("firstName").HasMaxLength(256).HasDefaultValue(string.Empty);
            builder.Property(e => e.LastName).IsRequired().HasColumnName("lastName").HasMaxLength(256).HasDefaultValue(string.Empty);
            builder.Property(e => e.Gender).IsRequired().HasColumnName("gender").HasDefaultValue(Gender.Unknown).SmartEnumConversion();
            builder.Property(e => e.Country).IsRequired().HasColumnName("country").HasDefaultValue(Country.Unknown).SmartEnumConversion();
            builder.Property(e => e.BirthDate).HasColumnName("birthDate").HasDefaultValue(null);
            builder.Property(e => e.UserRole).IsRequired().HasColumnName("role").HasDefaultValue(UserRole.User).SmartEnumConversion();
            builder.HasMany(e => e.RefreshTokens).WithOne(e => e.User).HasForeignKey(p => p.UserId);
        }
    }
}
