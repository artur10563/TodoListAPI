using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
using Todo.Infrastructure.Data.EntityTypeConfiguration.Shared;

namespace Todo.Infrastructure.Data.EntityTypeConfiguration
{
	internal class TodoListEntityTypeConfiguration : EntityBaseConfiguration<TodoList>
	{
		public override void Configure(EntityTypeBuilder<TodoList> builder)
		{
			base.Configure(builder);

			builder.Property(l => l.Title)
				.HasMaxLength(255)
				.IsRequired();

			builder.HasMany(l => l.Tasks)
				.WithOne(t => t.TodoList)
				.HasForeignKey(t => t.TodoListId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
