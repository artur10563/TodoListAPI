using MediatR;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.Auth.Commands.Register
{
	public sealed record RegisterCommand(string Nickname, string Email, string Password) : IRequest<Result>
	{
	}
}
