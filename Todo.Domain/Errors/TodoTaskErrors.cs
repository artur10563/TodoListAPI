namespace Todo.Domain.Errors
{
	public static class TodoTaskErrors
	{
		public static Error InvalidTitleLength = new("Invalid title length", "Title length must be 3-50 symbols");
	}
}
