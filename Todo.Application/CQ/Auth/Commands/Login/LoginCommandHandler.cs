using FluentValidation;
using MediatR;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.Auth.Commands.Login
{
	internal class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IValidator<LoginCommand> _validator;

		public LoginCommandHandler(IUserRepository userRepository, IValidator<LoginCommand> validator)
		{
			_userRepository = userRepository;
			_validator = validator;
		}

		public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{

			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return validationResult.Errors.FirstOrDefault().AsError();
			}

			var response = await _userRepository.LoginAsync(new DTOs.Auth.UserLoginDTO()
			{
				Email = request.Email,
				Password = request.Password
			});

			if (response.IsFailure) return response;

			return Result.Success(response.Value);
		}
	}
}
