using MediatR;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.CreateTodoList
{
	internal class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Result>
	{
		private readonly ITodoListRepository _todoRepository;
		private readonly IUnitOfWork _uow;

		public CreateTodoListCommandHandler(ITodoListRepository todoRepository, IUnitOfWork uow)
		{
			_todoRepository = todoRepository;
			_uow = uow;
		}

		public async Task<Result> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
		{
			var validationResult = CreateTodoListCommandValidator.Validate(request, _todoRepository);
			if (validationResult != Error.None)
			{
				return validationResult;
			}


			var newList = new TodoList()
			{
				Title = request.Title,
				OwnerId = request.OwnerId,
			};
			_todoRepository.Add(newList);
			await _uow.SaveChangesAsync();
			return Result.Success();
		}
	}

}
