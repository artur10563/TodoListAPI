namespace Todo.Domain.Errors
{
    public static class TodoListErrors
	{
		public static readonly Error InvalidTitleLength = new("Invalid title length", "Title length must be 3-50 symbols");
		public static readonly Error ListAllreadyExists = new("List allready exists", "List with this title allready exists");
	}
}
