namespace EyeSoft
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.Implementations;

	public static class Enforce
	{
		public static IParameterCondition Argument<T>(Expression<Func<T>> parameter)
		{
			return new ParameterCondition().Argument(parameter);
		}
	}
}