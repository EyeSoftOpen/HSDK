namespace EyeSoft
{
	using System;

	public interface INegableCondition<T>
		: IBooleanCondition<T>
	{
		IBooleanCondition<T> Not { get; }

		INegableCondition<T> WithException<TException>()
			where TException : Exception, new();
	}
}