using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpTestsEx
{
	public static class TypeExtensions
	{
		public static string DisplayName(this Type source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			var builder = new StringBuilder(100);
			builder.Append(source.Name.Split('`').First());
			var sourceTypeInfo = source.GetTypeInfo();
			if (sourceTypeInfo.IsGenericType)
			{
				builder.Append("<");
				builder.Append(sourceTypeInfo.GetGenericArguments().Select(t => t.Name).AsCommaSeparatedValues());
				builder.Append(">");
			}
			return builder.ToString();
		}
	}
}