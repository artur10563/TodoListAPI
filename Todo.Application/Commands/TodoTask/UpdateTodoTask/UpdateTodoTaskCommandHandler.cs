using FluentValidation;
using MediatR;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoTask.UpdateTodoTask
{
    internal class UpdateTodoTaskCommandHandler : IRequestHandler<UpdateTodoTaskCommand, Result>
    {
        public readonly ITodoTaskRepository _todoTaskRepository;
        public readonly IUnitOfWork _uow;
        public readonly ITodoListRepository _todoListRepository;
        public readonly IValidator<UpdateTodoTaskCommand> _validator;

        public UpdateTodoTaskCommandHandler(
            ITodoTaskRepository todoTaskRepository,
            IUnitOfWork uow,
            ITodoListRepository todoListRepository,
            IValidator<UpdateTodoTaskCommand> validator)
        {
            _todoTaskRepository = todoTaskRepository;
            _todoListRepository = todoListRepository;
            _uow = uow;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateTodoTaskCommand request, CancellationToken cancellationToken)
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

            task.Status = request.Status;
            await _uow.SaveChangesAsync();

            return Result.Success();
        }
    }
}
