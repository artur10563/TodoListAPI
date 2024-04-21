using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoList.Queries.GetTodoListById
{
    public sealed record GetTodoListByIdQuery(int ListId, int UserId) : IRequest<Result<TodoListDTO>>
    {
    }
}
