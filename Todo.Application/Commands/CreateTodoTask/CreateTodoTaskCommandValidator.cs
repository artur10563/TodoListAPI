using FluentValidation;
using Todo.Application.Extensions;
using Todo.Domain.Errors;

namespace Todo.Application.Commands.CreateTodoTask
{
	internal class CreateTodoTaskCommandValidator : AbstractValidator<CreateTodoTaskCommand>
	{
		public CreateTodoTaskCommandValidator()
		{
			RuleFor(task => task.Title)
				.Length(3, 50)
				.WithError(TodoTaskErrors.InvalidTitleLength);

		}
	}
}
