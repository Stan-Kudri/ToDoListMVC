using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.DBContext.Configurations
{
    public sealed class ToDoItemsConfiguration : EntityConfigurationBase<ToDoItems>
    {
        protected override void ConfigureCore(EntityTypeBuilder<ToDoItems> builder)
        {
            builder.ToTable("toDoItems");
            builder.Property(e => e.Description).IsRequired().HasMaxLength(128).HasColumnName("description");
            builder.Property(e => e.CreateDate).IsRequired().HasDefaultValue(DateTime.Now).HasColumnName("dateCreate");
            builder.Property(e => e.CompletDate).HasDefaultValue(null).HasColumnName("dataCompletion");
            builder.Property(e => e.IsCaseCompletion).IsRequired().HasDefaultValue(false).HasColumnName("isCompletion").HasColumnType("BOOLEAN");
            builder.HasOne(e => e.User).WithMany(e => e.ToDoItems).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
