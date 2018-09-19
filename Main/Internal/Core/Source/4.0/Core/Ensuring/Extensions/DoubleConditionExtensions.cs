namespace EyeSoft
{
	using System;

	public static class DoubleConditionExtensions
	{
		public static IEnsuring GreaterThan(this IBooleanCondition<double> condition, double compareTo)
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

		public static IEnsuring Positive(this IBooleanCondition<double> condition)
		{
			return
				GreaterThan(condition, -1);
		}

		public static IEnsuring GreaterOrEqualThan(this IBooleanCondition<double> condition, double value)
		{
			return
				GreaterThan(condition, value - 1);
		}

		public static IEnsuring Between(this IBooleanCondition<double> condition, double min, double max)
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

		public static IEnsuring GreaterThanZero(this IBooleanCondition<double> condition)
		{
			return
				GreaterThan(condition, 0);
		}
	}
}