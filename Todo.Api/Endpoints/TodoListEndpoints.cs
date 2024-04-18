using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Api.Extensions;
using Todo.Application.Commands.TodoList.CreateTodoList;
using Todo.Application.Commands.TodoList.DeleteTodoList;
using Todo.Application.Commands.TodoTask.CreateTodoTask;
using Todo.Application.Commands.TodoTask.UpdateTodoTask;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.DTOs.TodoTaskDTOs;
using Todo.Application.Queries.GetAllTodos;
using Todo.Application.Queries.GetTodoListById;
using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Api.Endpoints
{
	public static class TodoListEndpoints
	{
		public static void RegisterTodoListEndpoints(this IEndpointRouteBuilder app)
		{
			var group = app.MapGroup("api/lists").RequireAuthorization();

			/// <summary>
			/// Returns lists of current user (Id, Title, Ammount of tasks)
			/// </summary>
			group.MapGet("", GetAllTodos);

			/// <summary>
			/// Create new todo list
			/// </summary>
			group.MapPost("", CreateTodoList);

			/// <summary>
			/// Return list and its tasks. 403 if user is not owner of list
			/// </summary>
			group.MapGet("/{listId}", GetTodoListById);

			/// <summary>
			/// Deletes list and its tasks. 403 if user is not owner of list
			/// </summary>
			group.MapDelete("{listId}", DeleteTodoList);

			/// <summary>
			/// Creates new task. 403 if user is not owner of list
			/// </summary>
			group.MapPost("/{listId}/tasks", CreateTodoTask);

			/// <summary>
			/// Updates new task. 403 if user is not owner of list
			/// </summary>
			group.MapPatch("{listId}/tasks/{taskId}", UpdateTodoTask);

		}

		private static async Task<IResult> GetAllTodos(
			ISender sender,
			ClaimsPrincipal claimsPrincipal)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var query = new GetAllTodosQuery(userId);
			Result<ICollection<TodoListInfoDTO>> response = await sender.Send(query);

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
				return response.AsTypedErrorResult();
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
				return response.AsTypedErrorResult();
			}

			return TypedResults.Created();
		}

		private static async Task<IResult> DeleteTodoList(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new DeleteTodoListCommand(listId, userId);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}

			return TypedResults.Ok();
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
				return response.AsTypedErrorResult();
			}

			return TypedResults.Created();
		}

		private static async Task<IResult> UpdateTodoTask(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId,
			[FromRoute] int taskId,
			[FromBody] TodoTaskStatus status
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new UpdateTodoTaskCommand(status, listId, taskId, userId);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}

			return TypedResults.Ok();
		}
	}
}
