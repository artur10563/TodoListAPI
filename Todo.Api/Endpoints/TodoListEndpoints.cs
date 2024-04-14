using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Application.Commands.CreateTodoList;
using Todo.Application.Commands.CreateTodoTask;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.DTOs.TodoTaskDTOs;
using Todo.Application.Queries.GetAllTodos;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Api.Endpoints
{
	public static class TodoListEndpoints
	{
		public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapGet("api/lists", GetAllTodos);
			app.MapPost("api/lists", CreateTodoList).RequireAuthorization();
			app.MapPost("api/lists/{listId}/tasks", CreateTodoTask);
		}

		private static async Task<IResult> GetAllTodos(ISender sender)
		{
			var query = new GetAllTodosQuery();
			Result<ICollection<TodoListDTO>> response = await sender.Send(query);

			return response.Value.Count > 0
				? TypedResults.Ok(response.Value)
				: TypedResults.NoContent();
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

		private static async Task<IResult> CreateTodoTask(
			ISender sender,
			IValidator<CreateTodoTaskCommand> _validator,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId,
			[FromBody] CreateTodoTaskDTO newTask
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new CreateTodoTaskCommand(newTask.Title, listId, userId);

			var validationResult = await _validator.ValidateAsync(command);
			if (!validationResult.IsValid)
			{
				return TypedResults.ValidationProblem(validationResult.ToDictionary());
			}

			var response = await sender.Send(command);
			if (response.IsFailure)
			{
				if (response.Error == TodoListErrors.UserNotListOwner)
					return TypedResults.Forbid();

				if (response.Error == TodoListErrors.ListNotFound)
					return TypedResults.NotFound($"{response.Error.Code}; {response.Error.Description}");
				return TypedResults.BadRequest($"{response.Error.Code}; {response.Error.Description}");
			}


			return TypedResults.Created();
		}

	}
}
