using Todo.Application.Repositories;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure.DependencyInjection
{
	public static class StorageContainer
	{
		public static IServiceCollection AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			serviceCollection.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

			serviceCollection.AddSingleton<JwtTokenProvider>();
			serviceCollection
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped<IUserRepository, UserRepository>()
				.AddScoped<ITodoListRepository, TodoListRepository>()
				.AddScoped<ITodoTaskRepository, TodoTaskRepository>();


			return serviceCollection;
		}
	}
}
