using FluentValidation;
using MediatR;
using System.Security.Claims;
using Todo.Application.Commands.CreateTodoList;
using Todo.Application.DTOs;
using Todo.Application.Queries.GetAllTodos;

namespace Todo.Api.Endpoints
{
	public static class TodoListEndpoints
	{
		public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapGet("api/lists", GetAllTodos);
			app.MapPost("api/lists", CreateTodoList).RequireAuthorization();
		}

		private static async Task<IResult> GetAllTodos(ISender sender)
		{
			var query = new GetAllTodosQuery();
			var response = await sender.Send(query);

			return response.Value.Count > 0
				? TypedResults.Ok(response.Value)
				: TypedResults.NotFound("There are no TodoLists");
		}

		private static async Task<IResult> CreateTodoList(
			ISender sender,
			IValidator<CreateTodoListCommand> _validator,
			ClaimsPrincipal claimsPrincipal,
			CreateTodoListDTO newList)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new CreateTodoListCommand(newList.Title, userId);

			var validationResult = await _validator.ValidateAsync(command);
			if (!validationResult.IsValid)
			{
				return TypedResults.ValidationProblem(validationResult.ToDictionary());
			}

			var response = await sender.Send(command);
			if (response.IsSuccess)
			{
				return TypedResults.Created("Created successfully");
			}

			return TypedResults.UnprocessableEntity(response.Error.Code + " " + response.Error.Description);
		}
	}
}
