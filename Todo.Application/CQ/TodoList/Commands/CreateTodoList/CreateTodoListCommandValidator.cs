using FluentValidation;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Errors;

namespace Todo.Application.CQ.TodoList.Commands.CreateTodoList
{
    internal class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        public CreateTodoListCommandValidator(ITodoListRepository _todoRepository)
        {
            RuleFor(tl => tl.Title)
                .Length(3, 50)
                .WithError(TodoListErrors.InvalidTitleLength);

            RuleFor(command => command)
                .MustAsync(async (command, _) =>
                {
                    return await _todoRepository.IsTitleUniqueForUser(command.OwnerId, command.Title);
                })
                .WithError(TodoListErrors.ListAllreadyExists);
        }
    }
}
