namespace Todo.Domain.Errors
{
    public static class TodoListErrors
	{
		public static readonly Error InvalidTitleLength = new("Invalid title length", "Title length must be 3-50 symbols");
	}
}
