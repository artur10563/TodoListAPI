using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Data.EntityTypeConfiguration.Shared;

namespace TodoList.Infrastructure.Data.EntityTypeConfiguration
{
	internal class UserEntityTypeConfiguration : EntityBaseConfiguration<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			base.Configure(builder);

			builder.Property(u => u.Name)
				.HasMaxLength(25)
				.IsRequired(true);
			builder.Property(u => u.Email)
				.HasMaxLength(50)
				.IsRequired(true);

			builder.HasIndex(u => u.Email)
				.IsUnique();

			builder.Property(u => u.PasswordHash)
				.IsRequired();
		}
	}
}
