namespace EyeSoft
{
	using System;
	using System.Linq.Expressions;

	public interface IParameterCondition
	{
		IParameterCondition Argument<T>(Expression<Func<T>> expression);
	}
}