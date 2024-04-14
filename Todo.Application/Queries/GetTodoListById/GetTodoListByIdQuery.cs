using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetTodoListById
{
	public sealed record GetTodoListByIdQuery(int ListId, int UserId) : IRequest<Result<TodoListDTO>>
	{
	}
}
