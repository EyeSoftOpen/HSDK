namespace EyeSoft
{
	using System;

	public interface IConditionVerifierVerifiedException<T>
	{
		IConditionVerifierArgumentNotVerifiedException<T> VerifiedException<TExcpetion>(Func<TExcpetion> verifiedException)
			where TExcpetion : Exception;
	}
}