namespace EyeSoft
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

		public string Name { get; private set; }

		public Func<PropertyInfo, bool> Predicate { get; private set; }
	}
}