using MediatR;
using Todo.Application.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoList.DeleteTodoList
{
	internal class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, Result>
	{
		private readonly ITodoListRepository _todoListRepository;
		private readonly IUnitOfWork _uow;

		public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork uow)
		{
			_todoListRepository = todoListRepository;
			_uow = uow;
		}

		public async Task<Result> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
		{
			//Includes are required for cascade deletion
			var list = await _todoListRepository.GetByIdAsync(request.ListId);

			if (list == null)
			{
				return TodoListErrors.ListNotFound;
			}

			if (!list.IsListOwnedByUser(request.UserId))
			{
				return TodoListErrors.UserNotListOwner;
			}

			_todoListRepository.Remove(list);
			await _uow.SaveChangesAsync();

			return Result.Success();
		}
	}
}
