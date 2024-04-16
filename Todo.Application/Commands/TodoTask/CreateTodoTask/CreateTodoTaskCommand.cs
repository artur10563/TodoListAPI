using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoTask.CreateTodoTask
{
    public sealed record CreateTodoTaskCommand(string Title, int ListId, int UserId) : IRequest<Result>
    {
    }
}
