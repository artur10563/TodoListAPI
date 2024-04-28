using FluentValidation;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Errors;

namespace Todo.Application.CQ.Auth.Commands.Login
{
	internal class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator(IUserRepository _userRepository)
		{
			RuleFor(user => user.Password)
				.Length(5, 25)
				.WithError(UserErrors.InvalidPasswordLength);

			RuleFor(user => user.Email).MustAsync(async (email, CancellationToken) =>
			{
				var existingUser = await _userRepository.GetByEmailAsync(email);
				return existingUser is not null;
			})
				.WithError(UserErrors.UserNotFound);
		}
	}
}
