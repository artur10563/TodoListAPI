using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Application.Commands.CreateTodoList;
using Todo.Application.Commands.CreateTodoTask;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.DTOs.TodoTaskDTOs;
using Todo.Application.Queries.GetAllTodos;
using Todo.Application.Queries.GetTodoListById;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Api.Endpoints
{
	public static class TodoListEndpoints
	{
		public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder app)
		{
			var group = app.MapGroup("api/lists").RequireAuthorization();

			group.MapGet("", GetAllTodos); // all todos for current user WITHOUT TASKS
			group.MapPost("", CreateTodoList);

			group.MapGet("/{listId}", GetTodoListById); // full list (owner, tasks)
			group.MapPost("/{listId}/tasks", CreateTodoTask);

		}

		private static async Task<IResult> GetAllTodos(ISender sender)
		{
			var query = new GetAllTodosQuery();
			Result<ICollection<TodoListDTO>> response = await sender.Send(query);

			return response.Value.Count > 0
				? TypedResults.Ok(response.Value)
				: TypedResults.NoContent();
		}

		private static async Task<IResult> GetTodoListById(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var query = new GetTodoListByIdQuery(listId, userId);
			var response = await sender.Send(query);

			if (response.IsFailure)
			{
				if (response.Error == TodoListErrors.ListNotFound)
					return TypedResults.NotFound($"{response.Error.Code}; {response.Error.Description}");
				if (response.Error == TodoListErrors.UserNotListOwner)
					return TypedResults.Forbid();
				return TypedResults.BadRequest($"{response.Error.Code}; {response.Error.Description}");
			}
			return TypedResults.Ok(response.Value);

		}

		private static async Task<IResult> CreateTodoList(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			CreateTodoListDTO newList)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new CreateTodoListCommand(newList.Title, userId);
			var response = await sender.Send(command);
			if (response.IsFailure)
			{
				return TypedResults.BadRequest($"{response.Error.Code}; {response.Error.Description}");
			}

			return TypedResults.Created();
		}

		private static async Task<IResult> CreateTodoTask(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId,
			[FromBody] CreateTodoTaskDTO newTask
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new CreateTodoTaskCommand(newTask.Title, listId, userId);
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
