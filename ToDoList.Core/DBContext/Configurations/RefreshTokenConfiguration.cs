using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Authentication;
using ToDoList.Core.Models;

namespace ToDoList.Core.DBContext.Configurations
{
    public sealed class RefreshTokenConfiguration : EntityConfigurationBase<RefreshToken>
    {
        protected override void ConfigureCore(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refreshTokens");
            builder.Property(e => e.UserId).IsRequired().HasColumnName("userId");
            builder.Property(e => e.Token).HasDefaultValue(null).HasColumnName("refreshToken");
            builder.Property(e => e.Expires).IsRequired().HasColumnName("dateTimeExpires").HasDefaultValue(LoginConst.GetExpiresRefreshToken).HasColumnType("DATETIME");
            builder.Property(e => e.Create).IsRequired().HasColumnName("dateTimeCreate").HasDefaultValue(DateTime.UtcNow).HasColumnType("DATETIME");
            builder.HasOne(e => e.User).WithMany(e => e.RefreshTokens).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(e => e.Expired);
            builder.Ignore(e => e.ShouldUppdate);
        }
    }
}
