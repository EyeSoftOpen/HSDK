namespace EyeSoft
{
	using System;

	public static class BooleanConditionExtensions
	{
		public static IEnsuring True(this IBooleanCondition<bool> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(value => value)
					.ArgumentVerifiedException(argumentName => new ArgumentException("The condition is verified."))
					.VerifiedException(() => new ConditionNotVerifiedException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentException("The condition is not verified."))
					.NotVerifiedException(() => new ConditionVerifiedException())
					.Evaluate();
		}

		public static IEnsuring False(this IBooleanCondition<bool> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(value => !value)
					.ArgumentVerifiedException(argumentName => new ArgumentException("The condition is not verified."))
					.VerifiedException(() => new ConditionNotVerifiedException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentException("The condition is verified."))
					.NotVerifiedException(() => new ConditionVerifiedException())
					.Evaluate();
		}
	}
}