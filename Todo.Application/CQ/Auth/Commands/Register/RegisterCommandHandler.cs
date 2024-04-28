using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Todo.Application.Extensions;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.Auth.Commands.Register
{
	internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
	{
		private readonly IUserRepository _userRepository;
		private readonly IValidator<RegisterCommand> _validator;

		public RegisterCommandHandler(IUserRepository userRepository, IValidator<RegisterCommand> validator)
		{
			_userRepository = userRepository;
			_validator = validator;
		}

		public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{

			var validationResult = await _validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				return validationResult.Errors.FirstOrDefault().AsError();
			}

			var response = await _userRepository.RegisterAsync(new DTOs.Auth.UserRegisterDTO()
			{
				Nickname = request.Nickname,
				Email = request.Email,
				Password = request.Password,
			});

			if (response.IsSuccess)
			{
				return Result.Success(response);
			}
			return new Error(StatusCode: StatusCode.BadRequest);
		}
	}
}
