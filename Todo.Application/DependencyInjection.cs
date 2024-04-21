using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Application.CQ.TodoList.Commands.CreateTodoList;
using Todo.Application.CQ.TodoTask.Commands.CreateTodoTask;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTask;
using Todo.Application.Mapper;

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
                .AddScoped<IValidator<UpdateTodoTaskCommand>, UpdateTodoTaskStatusCommandValidator>()
                ;

            services
                .AddAutoMapper(typeof(TodoListProfile))
                .AddAutoMapper(typeof(TodoTaskProfile))
                .AddAutoMapper(typeof(UserProfile));

            return services;
        }
    }
}
