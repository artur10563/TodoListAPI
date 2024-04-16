using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.Commands.TodoList.DeleteTodoList
{
	public sealed record DeleteTodoListCommand(int ListId, int UserId) : IRequest<Result>
	{
	}
}
