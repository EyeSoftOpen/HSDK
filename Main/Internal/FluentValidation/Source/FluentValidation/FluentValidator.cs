namespace EyeSoft.FluentValidation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	using EyeSoft.Reflection;
	using EyeSoft.Validation;

	using global::FluentValidation;

	public abstract class FluentValidator<T> : AbstractValidator<T>, Validation.IValidator<T>
	{
		public new IEnumerable<ValidationError> Validate(T instance)
		{
			return
				base
					.Validate(instance)
					.Errors
					.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage, x.AttemptedValue))
					.ToList();
		}

		public IEnumerable<ValidationError> Validate(T instance, string propertyName)
		{
			return Validate(instance).Where(x => x.PropertyName == propertyName);
		}

		public IEnumerable<ValidationError> Validate<TProperty>(T instance, Expression<Func<T, TProperty>> property)
		{
			return Validate(instance, property.PropertyName());
		}
	}
}