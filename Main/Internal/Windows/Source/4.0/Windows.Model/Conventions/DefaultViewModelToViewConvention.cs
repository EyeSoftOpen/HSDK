namespace EyeSoft.Windows.Model.Conventions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Windows;

	public class DefaultViewModelToViewConvention : ViewModelToViewConvention
	{
		private static readonly Type windowType = typeof(Window);

		private readonly IDictionary<string, Type> windowTypes;

		private readonly string viewAssemblyName;

		public DefaultViewModelToViewConvention() : this(Assembly.GetEntryAssembly())
		{
		}

		public DefaultViewModelToViewConvention(Assembly viewAssembly)
		{
			windowTypes =
				viewAssembly.GetTypes().Where(type => type.IsSubclassOf(windowType))
					.ToDictionary(k => k.Name.Replace("Window", null), t => t);

			viewAssemblyName = viewAssembly.GetName().Name;
		}

		protected override Type TryMapTo(Type viewModelType)
		{
			var viewName = viewModelType.Name.Replace("ViewModel", null);

			if (!windowTypes.ContainsKey(viewName))
			{
				const string Message = "Cannot find a Window named '{0}' in the assembly '{1}' based on the ViewModel {2}";

				throw new ArgumentException(string.Format(Message, viewName, viewAssemblyName, viewModelType.FullName));
			}

			return windowTypes[viewName];
		}
	}
}