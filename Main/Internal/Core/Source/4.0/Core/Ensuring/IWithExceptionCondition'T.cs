namespace EyeSoft
{
	using System;

	public interface IWithExceptionCondition<T>
		: ICondition<T>
	{
		ICondition<T> WithException<TException>()
			where TException : Exception;

		ICondition<T> WithException<TException>(TException exception)
			where TException : Exception;

		ICondition<T> WithException<TException>(Action<TException> action)
			where TException : Exception, new();

		ICondition<T> WithException<TException>(string message, params object[] parameters)
			where TException : Exception, new();
	}
}