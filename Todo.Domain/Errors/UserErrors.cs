namespace Todo.Domain.Errors
{
	public static class UserErrors
	{
		public static Error UserAllreadyExists = new(StatusCode: StatusCode.Conflict, Description: "User allready exists");
		public static Error InvalidNicknameLength = new(StatusCode.BadRequest, "Nickname length must be 2-25 symbols");
		public static Error InvalidEmail = new(StatusCode.BadRequest, "Invalid email");
		public static Error InvalidPasswordLength = new(StatusCode.BadRequest, "Password length must be 5-25 symbols");
		public static Error UserNotFound = new(StatusCode.NotFound, "User with specified email is not found");
	}
}
