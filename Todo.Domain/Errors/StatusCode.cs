namespace Todo.Domain.Errors
{
	public enum StatusCode
	{
		Ok = 200,
		BadRequest = 400,
		Unauthorized = 401,
		Forbid = 403,
		NotFound = 404,
		Conflict = 409,
	}
}
