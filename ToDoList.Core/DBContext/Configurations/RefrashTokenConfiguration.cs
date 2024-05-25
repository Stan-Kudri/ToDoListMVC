using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Authentication;
using ToDoList.Core.Models;

namespace ToDoList.Core.DBContext.Configurations
{
    public sealed class RefrashTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refreshtoken");
            builder.HasKey(e => e.UserId);
            builder.Property(e => e.UserId).IsRequired().HasColumnName("userid");
            builder.Property(e => e.Token).HasDefaultValue(null).HasColumnName("refreshtoken");
            builder.Property(e => e.Expires).IsRequired().HasColumnName("datetimeexpires").HasDefaultValue(LoginConst.GetExpiresRefreshToken).HasColumnType("DATETIME");
            builder.Property(e => e.Create).IsRequired().HasColumnName("datetimecreate").HasDefaultValue(DateTime.UtcNow).HasColumnType("DATETIME");
            builder.HasOne(e => e.User).WithMany(e => e.RefreshTokens).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(e => e.Expired);
            builder.Ignore(e => e.ShouldUppdate);
        }
    }
}
