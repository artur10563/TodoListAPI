using Todo.Application.Repositories;
using Todo.Domain.Errors;


namespace Todo.Application.Commands.CreateTodoList
{
	public static class CreateTodoListCommandValidator
	{
		public static Error Validate(CreateTodoListCommand request, ITodoListRepository todoListRepository)
		{
			var existingList = todoListRepository.FirstOrDefault(list =>
				list.Title.Equals(request.Title, StringComparison.CurrentCultureIgnoreCase) &&
				list.OwnerId == request.OwnerId);

			if (existingList is not null)
			{
				return TodoListErrors.ListAllreadyExists;
			}

			if (request.Title.Length < 3 || request.Title.Length > 50)
			{
				return TodoListErrors.InvalidTitleLength;
			}

			return Error.None;
		}
	}

}
