namespace Todo.Domain.Errors
{
	public static class TodoTaskErrors
	{
		public static Error InvalidTitleLength = new(StatusCode.BadRequest, "Title length must be 3-50 symbols");
		public static Error TaskNotFound = new(StatusCode.NotFound, "Task with specified id is not found");
		public static Error TaskNotInList = new(StatusCode.BadRequest, "The task does not belong to the specified list.");
		public static Error InvalidStatus = new(StatusCode.BadRequest, "Invalid task status");
	}
}
