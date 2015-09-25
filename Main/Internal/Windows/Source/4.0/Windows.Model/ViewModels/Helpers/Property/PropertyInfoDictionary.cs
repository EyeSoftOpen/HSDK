using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property
{
	internal class PropertyInfoDictionary
	{
		private const BindingFlags Bindings = BindingFlags.Public | BindingFlags.Instance;

		private static readonly Type commandType = typeof(ICommand);

		private readonly Type baseViewModelType = typeof(ViewModel);

		private readonly IDictionary<string, PropertyInfo> propertyInfoDictionary = new Dictionary<string, PropertyInfo>();

		private readonly Type currentViewModelType;

		private readonly ViewModel viewModel;

		public PropertyInfoDictionary(ViewModel viewModel)
		{
			this.viewModel = viewModel;
			currentViewModelType = viewModel.GetType();

			var properties = GetPropertiesExcludingCommand();

			foreach (var property in properties)
			{
				var propertyName = property.Name;

				if (propertyInfoDictionary.ContainsKey(propertyName))
				{
					const string Message = "The property '{0}' of the ViewModel '{1}' is already declared in a base class.";

					throw new InvalidOperationException(string.Format(Message, propertyName, currentViewModelType.Name));
				}

				propertyInfoDictionary.Add(propertyName, property);
			}
		}

		public PropertyInfo this[string propertyName]
		{
			get
			{
				if (!propertyInfoDictionary.ContainsKey(propertyName))
				{
					const string Format = "The property '{0}' is not part of the ViewModel '{1}'.";
					var message = string.Format(Format, propertyName, currentViewModelType.FullName);
					throw new InvalidOperationException(message);
				}

				return propertyInfoDictionary[propertyName];
			}
		}

		public object GetPropertyValue(string propertyName)
		{
			var propertyInfo = propertyInfoDictionary[propertyName];

			var value = propertyInfo.GetValue(viewModel, null);

			return value;
		}

		private IEnumerable<PropertyInfo> GetPropertiesExcludingCommand()
		{
			return
				currentViewModelType.GetProperties(Bindings)
					.Where(p => p.DeclaringType != baseViewModelType && !commandType.IsAssignableFrom(p.PropertyType))
					.OrderBy(x => x.Name).ToArray();
		}
	}
}