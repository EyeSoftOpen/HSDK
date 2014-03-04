namespace EyeSoft.Reflection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public static class GenericReflectionExtensions
	{
		private const BindingFlags InstanceBindingFlags =
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		public static object Invoke<T>(this T instance, string methodName, params object[] parameters)
		{
			var methodInfo = GetMethodInfo(instance, methodName, parameters, false);

			return
				methodInfo
					.Invoke(instance, parameters);
		}

		public static object Invoke<T>(
			this T instance,
			string methodName,
			Type[] genericArguments,
			params object[] parameters)
		{
			var methodInfo = GetMethodInfo(instance, methodName, parameters, true);

			return
				methodInfo
					.MakeGenericMethod(genericArguments)
					.Invoke(instance, parameters);
		}

		private static MethodInfo GetMethodInfo<T>(
			T instance,
			string methodName,
			IEnumerable<object> parameters,
			bool isGeneric)
		{
			var parametersType = parameters.Select(p => p.GetType()).ToArray();

			var methodInfo =
				instance
					.GetType()
					.GetMethods(InstanceBindingFlags)
					.Single(
						method =>
							method.Name.Equals(methodName) &&
							method.IsGenericMethod.Equals(isGeneric) &&
							ParametersMatch(method.GetParameters(), parametersType));

			return methodInfo;
		}

		private static bool ParametersMatch(
			IList<ParameterInfo> declaredParameters,
			IList<Type> passedParameters)
		{
			if (declaredParameters.Count != passedParameters.Count)
			{
				return false;
			}

			for (var i = 0; i < declaredParameters.Count; i++)
			{
				var passedParameter = passedParameters[i];
				var declaredParameter = declaredParameters[i].ParameterType;

				var equalsOrSubclassOf = passedParameter.EqualsOrSubclassOf(declaredParameter);

				if (!equalsOrSubclassOf)
				{
					return false;
				}
			}

			return true;
		}
	}
}