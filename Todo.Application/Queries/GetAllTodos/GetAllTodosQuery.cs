using MediatR;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetAllTodos
{
	public sealed record GetAllTodosQuery() : IRequest<Result<ICollection<TodoList>>>;
}
