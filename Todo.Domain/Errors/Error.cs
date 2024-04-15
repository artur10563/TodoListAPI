namespace Todo.Domain.Errors
{
	public sealed record Error(StatusCode StatusCode, string? Description = null)
	{
		public static readonly Error None = new(StatusCode.Ok, string.Empty);
	}
}
