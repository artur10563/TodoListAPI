using FluentValidation;
using Todo.Domain.Errors;

namespace Todo.Application.Extensions
{
	public static class FluentValidationExtensions
	{
		public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>
			(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Error error)
		{
			if (error is null)
			{
				throw new ArgumentNullException(nameof(error), "The error is required");
			}

			return ruleBuilder.WithErrorCode(error.Code).WithMessage(error.Description);
		}
	}
}
