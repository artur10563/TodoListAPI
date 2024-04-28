using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Application.CQ.TodoList.Commands.CreateTodoList;
using Todo.Application.CQ.TodoTask.Commands.CreateTodoTask;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskTitle;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskStatus;
using Todo.Application.Mapper;
using Todo.Application.CQ.Auth.Commands.Register;
using Todo.Application.CQ.Auth.Commands.Login;

namespace Todo.Application
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
				.AddScoped<IValidator<UpdateTodoTaskStatusCommand>, UpdateTodoTaskStatusCommandValidator>()
				.AddScoped<IValidator<UpdateTodoTaskTitleCommand>, UpdateTodoTaskTitleCommandValidator>()
				.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>()
				.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>()
				;

			services
				.AddAutoMapper(typeof(TodoListProfile))
				.AddAutoMapper(typeof(TodoTaskProfile))
				.AddAutoMapper(typeof(UserProfile));

			return services;
		}
	}
}
