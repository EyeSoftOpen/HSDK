namespace EyeSoft
{
	using System;

	public static class StringConditionExtensions
	{
		public static IEnsuring NullOrWhiteSpace(this IBooleanCondition<string> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(string.IsNullOrWhiteSpace)
					.ArgumentVerifiedException(argumentName => new ArgumentException(argumentName))
					.VerifiedException(() => new ConditionNotVerifiedException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentNullException(argumentName))
					.NotVerifiedException(() => new NullReferenceException())
					.Evaluate();
		}
	}
}