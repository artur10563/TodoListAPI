using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskTitle
{
	public sealed record UpdateTodoTaskTitleCommand(string Title, int TaskId, int ListId, int UserId) : IRequest<Result>
	{
	}
}
