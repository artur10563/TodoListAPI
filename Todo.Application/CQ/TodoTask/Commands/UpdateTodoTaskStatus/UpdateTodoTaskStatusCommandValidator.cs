using FluentValidation;
using Todo.Application.Extensions;
using Todo.Domain.Errors;

namespace Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskStatus
{
    internal class UpdateTodoTaskStatusCommandValidator : AbstractValidator<UpdateTodoTaskStatusCommand>
    {
        public UpdateTodoTaskStatusCommandValidator()
        {
            RuleFor(task => task.Status)
                .IsInEnum()
                .WithError(TodoTaskErrors.InvalidStatus);

        }
    }
}
