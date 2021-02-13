namespace EyeSoft.Extensions
{
    using System.ComponentModel;
    using System.Diagnostics;

    public static class ObjectExtensions
	{
		[DebuggerStepThrough]
		public static IObjectExtender<T> Extend<T>(this T obj)
		{
			return new ObjectExtender<T>(obj);
		}

		[DebuggerStepThrough]
		public static TDestination Convert<TDestination>(this object obj)
		{
			if (obj == null)
			{
				return default(TDestination);
			}

			var sourceType = obj.GetType();
			var destinationType = typeof(TDestination);

			if (sourceType == typeof(string))
			{
				return (TDestination)TypeDescriptor.GetConverter(destinationType).ConvertFromString((string)obj);
			}

			var converter = TypeDescriptor.GetConverter(obj);

			if (converter.IsNull())
			{
				return (TDestination)obj;
			}

			if (!converter.CanConvertTo(destinationType))
			{
				return (TDestination)obj;
			}

			return (TDestination)converter.ConvertTo(obj, typeof(TDestination));
		}

		[DebuggerStepThrough]
		public static bool IsNull(this object obj)
		{
			return obj.IsDefault();
		}

		[DebuggerStepThrough]
		public static bool IsNotNull(this object obj)
		{
			return !obj.IsNull();
		}
	}
}