namespace EyeSoft.Linq.Expressions
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	internal class Parameter<T> : IParameter<T>
	{
		public Parameter(Expression<Func<T>> expression)
		{
			SetParameterNameFromExpression(expression);
		}

		public T Value { get; private set; }

		public string Name { get; private set; }

		public Type Type { get; private set; }

		private void SetParameterNameFromExpression(Expression<Func<T>> parameter)
		{
			var body = (MemberExpression)parameter.Body;

			Name = body.Member.Name;

			Value = parameter.Compile()();

			Type =
				EqualityComparer<T>.Default.Equals(Value, default(T)) ?
					body.Type :
					Value.GetType();
		}
	}
}