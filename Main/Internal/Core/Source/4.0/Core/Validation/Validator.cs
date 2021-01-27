namespace EyeSoft.Core.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Reflection;

    public abstract class Validator<T> : IValidator<T>
	{
		public virtual IEnumerable<ValidationError> Validate(T instance)
		{
			var errors = new List<ValidationError>();

			var properties =
				typeof(T)
					.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Select(x => x.Name);

			foreach (var propertyName in properties)
			{
				errors.AddRange(Validate(instance, propertyName));
			}

			return
				errors.Any(error => error == null) ?
					Enumerable.Empty<ValidationError>() :
					errors;
		}

		public abstract IEnumerable<ValidationError> Validate(T instance, string propertyName);

		public IEnumerable<ValidationError> Validate<TProperty>(T instance, Expression<Func<T, TProperty>> property)
		{
			return Validate(instance, property.PropertyName());
		}
	}
}