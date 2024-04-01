using Todo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{

		public DbSet<User> Users { get; set; }
		public DbSet<TodoList> TodoLists { get; set; }
		public DbSet<TodoTask> TodoTasks { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
			base.OnModelCreating(builder);
		}
	}
}
