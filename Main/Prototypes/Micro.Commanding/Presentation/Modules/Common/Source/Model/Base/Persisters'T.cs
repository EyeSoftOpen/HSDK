namespace Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public class Persisters<T>
	{
		private readonly IDictionary<string, Delegate> getCommandForProperty = new Dictionary<string, Delegate>();

		public void Add<TCommand>(Expression<Func<T, object>> expression, Func<TCommand> commandFromProperty)
		{
			var propertyName = EyeSoft.Reflection.Reflector.PropertyName(expression);
			getCommandForProperty.Add(propertyName, commandFromProperty);
		}

		public object this[string propertyName]
		{
			get
			{
				var command = getCommandForProperty[propertyName].DynamicInvoke();
				return command;
			}
		}
	}
}