namespace Todo.Domain.Errors
{
	public static class TodoListErrors
	{
		public static readonly Error InvalidTitleLength = new(StatusCode.BadRequest, "Title length must be 3-50 symbols");
		public static readonly Error ListAllreadyExists = new(StatusCode.Conflict, "List with this title allready exists");
		public static readonly Error ListNotFound = new(StatusCode.NotFound, "List with specified id is not found");
		public static readonly Error UserNotListOwner = new(StatusCode.Forbid, "User does not have permission to modify this list");
	}
}
