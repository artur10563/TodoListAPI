using FluentValidation;
using Todo.Application.Extensions;
using Todo.Domain.Errors;

namespace Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskTitle
{
	internal class UpdateTodoTaskTitleCommandValidator : AbstractValidator<UpdateTodoTaskTitleCommand>
	{
		public UpdateTodoTaskTitleCommandValidator()
		{
			RuleFor(task => task.Title)
				.Length(min: 3, max: 50)
				.WithError(TodoTaskErrors.InvalidTitleLength);
		}
	}
}
