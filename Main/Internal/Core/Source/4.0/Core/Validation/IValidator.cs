namespace EyeSoft.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface IValidator<T>
	{
		IEnumerable<ValidationError> Validate(T instance);

		IEnumerable<ValidationError> Validate(T instance, string propertyName);

		IEnumerable<ValidationError> Validate<TProperty>(T instance, Expression<Func<T, TProperty>> property);
	}
}