namespace EyeSoft
{
	using System;

	public static class ObjectConditionExtensions
	{
		public static IEnsuring Null(this IBooleanCondition<object> condition)
		{
			return
				Condition
					.On(condition)
					.Expression(x => x == null)
					.ArgumentVerifiedException(argumentName => new ArgumentException(argumentName))
					.VerifiedException(() => new ObjectNotNullException())
					.ArgumentNotVerifiedException(argumentName => new ArgumentNullException(argumentName))
					.NotVerifiedException(() => new NullReferenceException())
					.Evaluate();
		}
	}
}