using FluentValidation;
using MediatR;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoList.CreateTodoList
{
	internal class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Result>
	{
		private readonly ITodoListRepository _todoRepository;
		private readonly IUnitOfWork _uow;
		private readonly IValidator<CreateTodoListCommand> _validator;

		public CreateTodoListCommandHandler(
			ITodoListRepository todoRepository,
			IUnitOfWork uow,
			IValidator<CreateTodoListCommand> validator)
		{
			_todoRepository = todoRepository;
			_uow = uow;
			_validator = validator;
		}

		public async Task<Result> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return validationResult.Errors.FirstOrDefault().AsError();
			}

			var newList = new Domain.Entities.TodoList()
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
