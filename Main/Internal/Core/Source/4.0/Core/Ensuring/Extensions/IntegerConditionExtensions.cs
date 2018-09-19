namespace EyeSoft
{
	using System;

	public static class IntegerConditionExtensions
	{
		public static IEnsuring GreaterThan(this IBooleanCondition<int> condition, int compareTo)
		{
			return
				Condition
					.On(condition)
					.Expression(value => value > compareTo)
					.ArgumentVerifiedException(argumentName => new ArgumentOutOfRangeException(argumentName))
					.VerifiedException(() => new ArgumentOutOfRangeException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentOutOfRangeException(argumentName))
					.NotVerifiedException(() => new ArgumentOutOfRangeException())
					.Evaluate();
		}

		public static IEnsuring Positive(this IBooleanCondition<int> condition)
		{
			return
				GreaterThan(condition, -1);
		}

		public static IEnsuring GreaterOrEqualThan(this IBooleanCondition<int> condition, int value)
		{
			return
				GreaterThan(condition, value - 1);
		}

		public static IEnsuring Between(this IBooleanCondition<int> condition, int min, int max)
		{
			return
				Condition
				.On(condition)
				.Expression(value => value >= min && value <= max)
				.ArgumentVerifiedException(argumentName => new ArgumentOutOfRangeException(argumentName))
				.VerifiedException(() => new ArgumentOutOfRangeException())
				.ArgumentNotVerifiedException(argumentName => new ArgumentOutOfRangeException(argumentName))
				.NotVerifiedException(() => new ArgumentOutOfRangeException())
				.Evaluate();
		}

		public static IEnsuring GreaterThanZero(this IBooleanCondition<int> condition)
		{
			return
				GreaterThan(condition, 0);
		}
	}
}