using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Entities;
using Todo.Infrastructure.Data.EntityTypeConfiguration.Shared;

namespace Todo.Infrastructure.Data.EntityTypeConfiguration
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

			builder.HasMany(u => u.Lists)
				.WithOne(l => l.Owner)
				.HasForeignKey(u => u.OwnerId)
				.OnDelete(DeleteBehavior.Cascade);


		}
	}
}
