using MediatR;
using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskStatus
{
    public sealed record UpdateTodoTaskStatusCommand(TodoTaskStatus Status, int TaskId, int ListId, int UserId) : IRequest<Result>
    {
    }
}
