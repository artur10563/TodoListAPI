namespace Todo.Domain.Errors
{
	public static class TodoListErrors
	{
		public static readonly Error InvalidTitleLength = new("Invalid title length", "Title length must be 3-50 symbols");
		public static readonly Error ListAllreadyExists = new("List allready exists", "List with this title allready exists");
		public static readonly Error ListNotFound = new("List not found", "List with specified id is not found");
		public static readonly Error UserNotListOwner = new("User is not the owner of the list", "User does not have permission to modify this list");
	}
}
