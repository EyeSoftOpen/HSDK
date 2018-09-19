namespace EyeSoft
{
	using System;

	public interface IConditionVerifierArgumentVerifiedException<T>
	{
		IConditionVerifierVerifiedException<T> ArgumentVerifiedException<TArgumentException>(
			Func<string, TArgumentException> argumentVerifiedException)
				where TArgumentException : ArgumentException;
	}
}