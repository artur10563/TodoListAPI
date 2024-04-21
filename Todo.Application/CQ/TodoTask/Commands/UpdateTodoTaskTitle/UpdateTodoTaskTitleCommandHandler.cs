using FluentValidation;
using MediatR;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskStatus;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskTitle
{
	internal class UpdateTodoTaskTitleCommandHandler : IRequestHandler<UpdateTodoTaskTitleCommand, Result>
	{
		public readonly ITodoTaskRepository _todoTaskRepository;
		public readonly IUnitOfWork _uow;
		public readonly ITodoListRepository _todoListRepository;
		public readonly IValidator<UpdateTodoTaskTitleCommand> _validator;

		public UpdateTodoTaskTitleCommandHandler(
			ITodoTaskRepository todoTaskRepository,
			IUnitOfWork uow,
			ITodoListRepository todoListRepository,
			IValidator<UpdateTodoTaskTitleCommand> validator)
		{
			_todoTaskRepository = todoTaskRepository;
			_todoListRepository = todoListRepository;
			_uow = uow;
			_validator = validator;
		}
		public async Task<Result> Handle(UpdateTodoTaskTitleCommand request, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return validationResult.Errors.FirstOrDefault().AsError();
			}

			var task = await _todoTaskRepository.GetByIdAsync(request.TaskId);
			if (task is null)
			{
				return TodoTaskErrors.TaskNotFound;
			}
			if (task.IsInList(request.ListId))
			{
				return TodoTaskErrors.TaskNotInList;
			}

			var list = await _todoListRepository.GetByIdAsync(request.ListId);
			if (list is null)
			{
				return TodoListErrors.ListNotFound;
			}

			if (!list.IsListOwnedByUser(request.UserId))
			{
				return TodoListErrors.UserNotListOwner;
			}

			task.Title = request.Title;
			await _uow.SaveChangesAsync();

			return Result.Success();
		}
	}
}
