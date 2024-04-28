using Todo.Application.DTOs.Auth;
using MediatR;
using Todo.Application.CQ.Auth.Commands.Register;
using Todo.Api.Extensions;
using Todo.Application.CQ.Auth.Commands.Login;

namespace Todo.Api.Endpoints
{
	public static class AuthEndpoints
	{

		public static void RegisterAuthEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapPost("api/auth/register", RegisterUserAsync);
			app.MapPost("api/auth/login", LoginUserAsync);
		}

		private static async Task<IResult> RegisterUserAsync(
			ISender sender,
			UserRegisterDTO registerDTO)
		{

			var command = new RegisterCommand(registerDTO.Nickname, registerDTO.Email, registerDTO.Password);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}
			return TypedResults.Created();
		}

		private static async Task<IResult> LoginUserAsync(
			ISender sender,
			UserLoginDTO loginDTO)
		{
			var command = new LoginCommand(loginDTO.Email, loginDTO.Password);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}
			return TypedResults.Ok(response.Value);

		}


	}
}
