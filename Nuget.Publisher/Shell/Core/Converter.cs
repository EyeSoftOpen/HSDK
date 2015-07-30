namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.ComponentModel;
	using System.Reflection;

	public static class Converter
	{
		public static T Convert<T>(object value)
		{
			var type = typeof(T);

			var typeDescriptor = TypeDescriptor.GetConverter(type);

			T result;

			if (typeDescriptor.CanConvertTo(type))
			{
				result = (T)typeDescriptor.ConvertTo(value, type);
				return result;
			}

			var parseMethod = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);

			if (parseMethod == null)
			{
				throw new InvalidOperationException("Cannot use a TypeDescriptor or a Parse method to convert the object.");
			}

			try
			{
				result = (T)parseMethod.Invoke(null, new object[] { value });
				return result;
			}
			catch
			{
				throw;
			}

		}
	}
}