using FluentValidation;
using Todo.Application.Extensions;
using Todo.Domain.Errors;

namespace Todo.Application.Commands.UpdateTodoTask
{
	internal class UpdateTodoTaskCommandValidator : AbstractValidator<UpdateTodoTaskCommand>
	{
		public UpdateTodoTaskCommandValidator()
		{
			RuleFor(task => task.Status)
				.IsInEnum()
				.WithError(TodoTaskErrors.InvalidStatus);

		}
	}
}
