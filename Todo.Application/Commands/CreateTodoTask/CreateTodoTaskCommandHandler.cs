using MediatR;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.CreateTodoTask
{
	internal class CreateTodoTaskCommandHandler : IRequestHandler<CreateTodoTaskCommand, Result>
	{
		private readonly IUnitOfWork _uow;
		private readonly ITodoListRepository _todoRepository;
		public CreateTodoTaskCommandHandler(IUnitOfWork uow, ITodoListRepository todoRepository)
		{
			_uow = uow;
			_todoRepository = todoRepository;
		}

		public async Task<Result> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
		{
			var list = await _todoRepository.GetByIdAsync(request.ListId);
			if (list is null)
			{
				return TodoListErrors.ListNotFound;
			}

			if (list.OwnerId != request.OwnerId)
			{
				return TodoListErrors.UserNotListOwner;
			}

			var newTask = new TodoTask()
			{
				Title = request.Title,
				TodoListId = list.Id,
			};

			list.Tasks.Add(newTask);
			await _uow.SaveChangesAsync();

			return Result.Success();
		}
	}
}
