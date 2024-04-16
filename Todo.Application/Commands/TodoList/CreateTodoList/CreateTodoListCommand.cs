using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoList.CreateTodoList
{
    public sealed record CreateTodoListCommand(string Title, int OwnerId) : IRequest<Result>
    {
    }
}
