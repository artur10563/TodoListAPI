using FluentValidation;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Errors;

namespace Todo.Application.CQ.Auth.Commands.Register
{
	internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
	{
		public RegisterCommandValidator(IUserRepository _userRepository)
		{
			RuleFor(user => user.Nickname)
				.Length(2, 25)
				.WithError(UserErrors.InvalidNicknameLength);

			RuleFor(user => user.Password)
				.Length(5, 25)
				.WithError(UserErrors.InvalidPasswordLength);

			RuleFor(user => user.Email).MustAsync(async (email, CancellationToken) =>
			{
				var existingUser = await _userRepository.GetByEmailAsync(email);
				return existingUser is null;
			})
				.WithError(UserErrors.UserAllreadyExists);
		}
	}
}
