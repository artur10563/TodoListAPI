using FluentValidation;
using MediatR;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoTask.CreateTodoTask
{
    internal class CreateTodoTaskCommandHandler : IRequestHandler<CreateTodoTaskCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITodoListRepository _todoRepository;
        private readonly IValidator<CreateTodoTaskCommand> _validator;
        public CreateTodoTaskCommandHandler(
            IUnitOfWork uow,
            ITodoListRepository todoRepository,
            IValidator<CreateTodoTaskCommand> validator)
        {
            _uow = uow;
            _todoRepository = todoRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateTodoTaskCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.FirstOrDefault().AsError();
            }

            var list = await _todoRepository.GetByIdAsync(request.ListId);
            if (list is null)
            {
                return TodoListErrors.ListNotFound;
            }

            if (list.IsListOwnedByUser(request.UserId))
            {
                return TodoListErrors.UserNotListOwner;
            }


            var newTask = new Domain.Entities.TodoTask()
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
