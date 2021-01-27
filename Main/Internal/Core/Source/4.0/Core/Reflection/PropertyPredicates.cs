namespace EyeSoft.Core.Reflection
{
    using System;
    using System.Reflection;
    using Extensions;

    public static class PropertyPredicates
	{
		public static readonly Lazy<Func<PropertyInfo, bool>> CanReadAndWrite =
			new Lazy<Func<PropertyInfo, bool>>(() => p => p.GetGetMethod() != null && p.CanWrite);

		public static readonly Lazy<PropertyPredicate> String =
			new Lazy<PropertyPredicate>(
				() => new PropertyPredicate("String", p => p.PropertyType.IsString() && CanReadAndWrite.Value(p)));

		public static readonly Lazy<PropertyPredicate> Primitive =
			new Lazy<PropertyPredicate>(
				() => new PropertyPredicate("Primitive", p => p.PropertyType.IsValueTypeOrString() && CanReadAndWrite.Value(p)));

		public static readonly Lazy<PropertyPredicate> Reference =
			new Lazy<PropertyPredicate>(
				() => new PropertyPredicate("Reference", p => p.CanRead && !p.PropertyType.IsValueTypeOrString()));
	}
}