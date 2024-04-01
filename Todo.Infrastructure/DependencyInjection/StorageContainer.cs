using Todo.Application.Repositories;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Data.Repositories;
using Todo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

			serviceCollection.AddScoped<IUserRepository, UserRepository>();
			serviceCollection.AddSingleton<JwtTokenProvider>();

			return serviceCollection;
		}
	}
}
