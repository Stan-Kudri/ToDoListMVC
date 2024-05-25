using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Models.Affair;

namespace ToDoList.Core.DBContext.Configurations
{
    public sealed class AffairsConfiguration : EntityConfigurationBase<Affairs>
    {
        protected override void ConfigureCore(EntityTypeBuilder<Affairs> builder)
        {
            builder.ToTable("affairs");
            builder.Property(e => e.Description).IsRequired().HasMaxLength(128).HasColumnName("description");
            builder.Property(e => e.DateCreate).IsRequired().HasDefaultValue(DateTime.Now).HasColumnName("datecreate");
            builder.Property(e => e.DateCompletion).HasDefaultValue(null).HasColumnName("datacompletion");
            builder.Property(e => e.IsCaseCompletion).IsRequired().HasDefaultValue(false).HasColumnName("iscompletion").HasColumnType("BOOLEAN");
            builder.HasOne(e => e.User).WithMany(e => e.Affairs).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
