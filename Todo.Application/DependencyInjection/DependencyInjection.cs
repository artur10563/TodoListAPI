using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Application.Commands.CreateTodoList;

namespace Todo.Application.DependencyInjection
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{

			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			//services.AddValidatorsFromAssemblyContaining<CreateTodoListCommandValidator2>();
			services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddScoped<IValidator<CreateTodoListCommand>, CreateTodoListCommandValidator>();

			return services;
		}
	}
}
