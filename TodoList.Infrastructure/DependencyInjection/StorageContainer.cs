using TodoList.Application.Repositories;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Data.Repositories;
using TodoList.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Infrastructure.DependencyInjection
{
	public static class StorageContainer
	{
		public static IServiceCollection AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			serviceCollection.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

			serviceCollection.AddScoped<IUserRepository, UserRepository>();
			serviceCollection.AddSingleton<JwtTokenProvider>();

			return serviceCollection;
		}
	}
}
