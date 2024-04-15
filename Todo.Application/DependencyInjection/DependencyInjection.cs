using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Application.Commands.CreateTodoList;
using Todo.Application.Commands.CreateTodoTask;
using Todo.Application.Commands.UpdateTodoTask;
using Todo.Application.Mapper;

namespace Todo.Application.DependencyInjection
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{

			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services
				.AddScoped<IValidator<CreateTodoListCommand>, CreateTodoListCommandValidator>()
				.AddScoped<IValidator<CreateTodoTaskCommand>, CreateTodoTaskCommandValidator>()
				.AddScoped<IValidator<UpdateTodoTaskCommand>, UpdateTodoTaskCommandValidator>()
				;

			services
				.AddAutoMapper(typeof(TodoListProfile))
				.AddAutoMapper(typeof(TodoTaskProfile))
				.AddAutoMapper(typeof(UserProfile));

			return services;
		}
	}
}
