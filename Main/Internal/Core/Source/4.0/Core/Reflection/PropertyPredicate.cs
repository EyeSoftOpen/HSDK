namespace EyeSoft.Reflection
{
    using System;
    using System.Reflection;

    public class PropertyPredicate
	{
		public PropertyPredicate(string name, Func<PropertyInfo, bool> predicate)
		{
			Name = name;
			Predicate = predicate;
		}

		public string Name { get; }

		public Func<PropertyInfo, bool> Predicate { get; }
	}
}