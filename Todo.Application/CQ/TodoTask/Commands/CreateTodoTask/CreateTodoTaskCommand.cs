using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoTask.Commands.CreateTodoTask
{
    public sealed record CreateTodoTaskCommand(string Title, int ListId, int UserId) : IRequest<Result>
    {
    }
}
