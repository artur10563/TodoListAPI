using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Infrastructure.Data.EntityTypeConfiguration.Shared;

namespace Todo.Infrastructure.Data.EntityTypeConfiguration
{
	internal class TodoTaskEntityTypeConfiguration : EntityBaseConfiguration<TodoTask>
	{
		public override void Configure(EntityTypeBuilder<TodoTask> builder)
		{
			base.Configure(builder);

			builder.Property(t => t.Title)
				.HasMaxLength(255)
				.IsRequired();

			builder.Property(t => t.Status)
				.HasDefaultValue(TodoTaskStatus.Todo)
				.IsRequired();
		}
	}
}
