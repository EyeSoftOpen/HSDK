namespace EyeSoft
{
	using System;

	public interface IConditionVerifierArgumentNotVerifiedException<T>
	{
		IConditionVerifierNotVerifiedException<T>
			ArgumentNotVerifiedException<TArgumentException>(Func<string, TArgumentException> argumentNotVerifiedException)
			where TArgumentException : ArgumentException;
	}
}