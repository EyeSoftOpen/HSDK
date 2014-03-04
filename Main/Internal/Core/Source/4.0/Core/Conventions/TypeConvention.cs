namespace EyeSoft.Conventions
{
	using System;

	public abstract class TypeConvention<TSource, TDestination>
	{
		public Type MapTo(Type type)
		{
			if (!type.EqualsOrSubclassOf(typeof(TSource)))
			{
				new ArgumentException("The source type must be of type {0}.".NamedFormat(type)).Throw();
			}

			var mapTo = TryMapTo(type);

			if (!mapTo.EqualsOrSubclassOf(typeof(TDestination)))
			{
				new ArgumentException("The destination type must be of type {0}.".NamedFormat(type)).Throw();
			}

			return mapTo;
		}

		protected abstract Type TryMapTo(Type type);
	}
}