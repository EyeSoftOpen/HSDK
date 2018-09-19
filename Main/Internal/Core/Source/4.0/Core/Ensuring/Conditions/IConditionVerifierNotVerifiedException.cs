namespace EyeSoft
{
	using System;

	public interface IConditionVerifierNotVerifiedException<T>
	{
		IConditionVerifierEvaluate<T> NotVerifiedException<TException>(Func<TException> notVerifiedException)
			where TException : Exception;
	}
}