﻿using TodoList.Infrastructure.Data;

namespace TodoList.Api.Endpoints
{
	public static class UserEndpoints
	{
		public static void RegisterUserEndpoints(this IEndpointRouteBuilder app)
		{
			app.MapGet("api/users", GetUsers).RequireAuthorization(); // Auth: "Bearer [token]"
		}

		private static async Task<IResult> GetUsers(AppDbContext _context)
		{
			//Example, replace with DTOs
			var users = _context.Users;
			return TypedResults.Ok(users);
		}
	}
}
