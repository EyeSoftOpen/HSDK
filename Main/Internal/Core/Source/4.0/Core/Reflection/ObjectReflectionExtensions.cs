namespace EyeSoft.Reflection
{
	using System;
	using System.Reflection;

	using EyeSoft.Extensions;

	public static class ObjectReflectionExtensions
	{
		public static T GetField<T>(this object obj, string name)
		{
			return
				obj
					.GetType()
					.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.GetValue(obj)
					.Convert<T>();
		}

		public static Field<T> GetField<T>(this object obj)
		{
			if (obj.GetType().Name == "RuntimeType")
			{
				return
					EyeSoft
						.TypeExtensions
						.GetField<T>((Type)obj);
			}

			return
				EyeSoft
					.TypeExtensions
					.GetField<T>(obj.GetType());
		}

		public static object GetPropertyValue(this object obj, string propertyName)
		{
			var propertyInfo =
				obj
					.GetType()
					.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

			return
				propertyInfo
					.GetValue(obj, null);
		}
	}
}