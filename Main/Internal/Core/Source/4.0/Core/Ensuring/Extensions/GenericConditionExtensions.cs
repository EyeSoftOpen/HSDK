namespace EyeSoft
{
	using System;

	using EyeSoft.Extensions;

	public static class GenericConditionExtensions
	{
		public static IEnsuring Null<T>(this IBooleanCondition<T> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(x => x.IsDefault())
					.ArgumentVerifiedException(argumentName => new ArgumentException(argumentName))
					.VerifiedException(() => new ObjectNotNullException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentNullException(argumentName))
					.NotVerifiedException(() => new NullReferenceException())
					.Evaluate();
		}

		public static IEnsuring EqualsTo<T>(this IBooleanCondition<T> condition, T other)
			where T : IEquatable<T>
		{
			var localValue = default(T);

			return
				Condition
					.On(condition)
					.Expression(value => { localValue = value; return value.Equals(other); })
					.ArgumentVerifiedException(argumentName => new ArgumentException("Objects are equals."))
					.VerifiedException(() => new NotEqualException(localValue, other))
					.ArgumentNotVerifiedException(argumentName => new ArgumentException("Object are not equals.", argumentName))
					.NotVerifiedException(() => new Exception("Objects are not equals."))
					.Evaluate();
		}

		public static IEnsuring Default<T>(this IBooleanCondition<T> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(value => value.IsDefault())
					.ArgumentVerifiedException(argumentName => new ArgumentNullException(argumentName))
					.VerifiedException(() => new ObjectNotNullException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentNullException(argumentName))
					.NotVerifiedException(() => new NullReferenceException())
					.Evaluate();
		}
	}
}