using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Api.Extensions
{
	public static class ResultExtension
	{
		public static IResult AsTypedErrorResult(this Result result)
		{
			if (result.IsSuccess) throw new ArgumentException("Can`t generate error for successfull result");

			switch (result.Error.StatusCode)
			{
				case StatusCode.NotFound:
					return TypedResults.NotFound(result.Error);
				case StatusCode.Conflict:
					return TypedResults.Conflict(result.Error);
				case StatusCode.BadRequest:
					return TypedResults.BadRequest(result.Error);
				default: return TypedResults.StatusCode((int)result.Error.StatusCode);
			}
		}
	}
}
