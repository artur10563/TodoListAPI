using MediatR;
using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoTask.UpdateTodoTask
{
    public sealed record UpdateTodoTaskCommand(TodoTaskStatus Status, int TaskId, int ListId, int UserId) : IRequest<Result>
    {
    }
}
