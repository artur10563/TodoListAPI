using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.Auth.Commands.Login
{
	public sealed record LoginCommand(string Email, string Password) : IRequest<Result<string>>
	{
	}
}
