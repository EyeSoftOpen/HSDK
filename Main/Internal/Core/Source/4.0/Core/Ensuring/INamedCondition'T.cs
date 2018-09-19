namespace EyeSoft
{
	using System;
	using System.Linq.Expressions;

	public interface INamedCondition<T>
		: IWithExceptionCondition<T>
	{
		IWithExceptionCondition<T> Named(string name);

		IWithExceptionCondition<T> Named<TArgument>(Expression<Func<TArgument>> expression);
	}
}