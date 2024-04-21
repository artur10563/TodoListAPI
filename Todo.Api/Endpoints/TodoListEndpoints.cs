using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Api.Extensions;
using Todo.Application.CQ.TodoList.Commands.CreateTodoList;
using Todo.Application.CQ.TodoList.Commands.DeleteTodoList;
using Todo.Application.CQ.TodoList.Queries.GetAllTodos;
using Todo.Application.CQ.TodoList.Queries.GetTodoListById;
using Todo.Application.CQ.TodoTask.Commands.CreateTodoTask;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskStatus;
using Todo.Application.CQ.TodoTask.Commands.UpdateTodoTaskTitle;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.DTOs.TodoTaskDTOs;
using Todo.Domain.Enums;
using Todo.Domain.Primitives;

namespace Todo.Api.Endpoints
{
	public static class TodoListEndpoints
	{
		#region Endpoints
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
			/// Updates status of a task. 403 if user is not owner of list
			/// </summary>
			group.MapPatch("{listId}/tasks/{taskId}/status", UpdateTodoTaskStatus);
			/// <summary>
			/// Updates title of a task. 403 if user is not owner of list
			/// </summary>
			group.MapPatch("{listId}/tasks/{taskId}/title", UpdateTodoTaskTitle);

		}
		#endregion
		#region Implementations
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

		private static async Task<IResult> UpdateTodoTaskStatus(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId,
			[FromRoute] int taskId,
			[FromBody] TodoTaskStatus status
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new UpdateTodoTaskStatusCommand(status, listId, taskId, userId);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}

			return TypedResults.Ok();
		}

		private static async Task<IResult> UpdateTodoTaskTitle(
			ISender sender,
			ClaimsPrincipal claimsPrincipal,
			[FromRoute] int listId,
			[FromRoute] int taskId,
			[FromBody] string title
			)
		{
			var userId = int.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)!);
			var command = new UpdateTodoTaskTitleCommand(title, listId, taskId, userId);
			var response = await sender.Send(command);

			if (response.IsFailure)
			{
				return response.AsTypedErrorResult();
			}

			return TypedResults.Ok();
		}
		#endregion
	}
}
