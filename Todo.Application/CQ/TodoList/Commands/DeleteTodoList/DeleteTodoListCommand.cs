using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoList.Commands.DeleteTodoList
{
    public sealed record DeleteTodoListCommand(int ListId, int UserId) : IRequest<Result>
    {
    }
}
