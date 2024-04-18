using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetAllTodos
{
	public sealed record GetAllTodosQuery(int UserId) : IRequest<Result<ICollection<TodoListInfoDTO>>>;
}
