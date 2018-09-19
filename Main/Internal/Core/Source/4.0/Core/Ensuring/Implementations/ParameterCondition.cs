namespace EyeSoft.Implementations
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.Linq.Expressions;

	internal class ParameterCondition : IParameterCondition
	{
		public IParameterCondition Argument<T>(Expression<Func<T>> parameter)
		{
			var arg = parameter.Parameter();

			var typeArgument = arg.Type == typeof(string) ? arg.Type : typeof(T);

			var conditionTreeType = typeof(ConditionTree<>).MakeGenericType(typeArgument);

			var conditionTree = Activator.CreateInstance(conditionTreeType, arg.Value, arg.Name);

			if (arg.Type == typeof(string))
			{
				((ConditionTree<string>)conditionTree).Is.Not.NullOrWhiteSpace();
			}
			else
			{
				((ConditionTree<T>)conditionTree).Is.Not.Default();
			}

			return new ParameterCondition();
		}
	}
}