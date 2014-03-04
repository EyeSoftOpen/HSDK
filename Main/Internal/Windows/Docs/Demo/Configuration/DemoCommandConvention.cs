namespace EyeSoft.Wpf.Facilities.Demo.Configuration
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	using EyeSoft.Windows.Model.Input;

	internal class DemoCommandConvention : CommandConvention
	{
		private static readonly IDictionary<Type, HashSet<MethodInfo>> commands =
			new Dictionary<Type, HashSet<MethodInfo>>();

		protected override CommandMethod GetMethod(
			Type viewModelType,
			string actionName,
			Type returnType,
			bool isCanExecuteMethod)
		{
			var method = base.GetMethod(viewModelType, actionName, returnType, isCanExecuteMethod);

			if (isCanExecuteMethod && method == null)
			{
				return null;
			}

			if (!commands.ContainsKey(viewModelType))
			{
				commands.Add(viewModelType, new HashSet<MethodInfo>());
			}

			commands[viewModelType].Add(method.MethodInfo);

			return method;
		}
	}
}