namespace EyeSoft.Reflection
{
	using System;
	using System.Linq;
	using System.Reflection;

	public static class MethodInfoExtensions
	{
		private const string SetPropertyMethodPrefix = "set_";
		private const string GetPropertyMethodPrefix = "get_";

		public static bool IsPropertySet(this MethodInfo methodInfo)
		{
			return
				methodInfo.IsSpecialName &&
				methodInfo.Name.StartsWith(SetPropertyMethodPrefix);
		}

		public static bool IsPropertyGet(this MethodInfo methodInfo)
		{
			return
				methodInfo.IsSpecialName &&
				methodInfo.Name.StartsWith(GetPropertyMethodPrefix);
		}

		public static string PropertyName(this MethodInfo methodInfo)
		{
			return methodInfo.Name.Substring(SetPropertyMethodPrefix.Length);
		}

		public static T Invoke<T>(this MethodInfo methodInfo, params object[] parameters)
		{
			var declaringType = methodInfo.DeclaringType;

			var obj =
				declaringType
					.GetConstructor(Type.EmptyTypes)
					.Invoke(null);

			return
				(T)methodInfo.Invoke(obj, parameters);
		}

		public static bool HasParameters(this MethodInfo methodInfo)
		{
			return methodInfo.GetParameters().Any();
		}
	}
}