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
			var types = viewAssembly.GetTypes();//GetTypesSafely(viewAssembly);

			windowTypes = WindowDictionary(types);

			viewAssemblyName = viewAssembly.GetName().Name;
		}

		public DefaultViewModelToViewConvention(params Type[] windows)
		{
			windowTypes = WindowDictionary(windows);
		}

		protected override Type TryMapTo(Type viewModelType)
		{
			var viewName = viewModelType.Name.Replace("ViewModel", null);

			if (windowTypes.ContainsKey(viewName))
			{
				return windowTypes[viewName];
			}

			const string Message = "Cannot find a Window named '{0}' in the assembly '{1}' based on the ViewModel {2}";

			throw new ArgumentException(string.Format(Message, viewName, viewAssemblyName, viewModelType.FullName));
		}

		private static IDictionary<string, Type> WindowDictionary(IEnumerable<Type> types)
		{
			return types.Where(type => type.IsSubclassOf(windowType))
				.ToDictionary(k => k.Name.Replace("Window", null), t => t);
		}

		private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
		{
			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException exception)
			{
				return exception.Types.Where(x => x != null);
			}
		}
	}
}