using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Core.Models;

namespace ToDoList.Core.DBContext.Configurations
{
    public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            ConfigureCore(builder);
        }

        protected abstract void ConfigureCore(EntityTypeBuilder<T> builder);
    }
}
