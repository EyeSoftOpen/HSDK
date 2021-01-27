namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    internal class PropertyInfoDictionary
	{
		private const BindingFlags Bindings = BindingFlags.Public | BindingFlags.Instance;

		private static readonly Type commandType = typeof(ICommand);

		private readonly Type baseViewModelType = typeof(ViewModel);

		private readonly IDictionary<string, PropertyInfo> propertyInfoDictionary = new Dictionary<string, PropertyInfo>();

	    private readonly IDictionary<string, PropertyInfo> commandsPropertyInfoDictionary = new Dictionary<string, PropertyInfo>();

        private readonly Type currentViewModelType;

		private readonly ViewModel viewModel;

		public PropertyInfoDictionary(ViewModel viewModel)
		{
			this.viewModel = viewModel;

			currentViewModelType = viewModel.GetType();

			var properties = GetAllProperties();

		    var allPropertiesExcludedCommands = properties.Where(p => !commandType.IsAssignableFrom(p.PropertyType)).ToArray();

		    var allPropertiesCommands = properties.Where(p => commandType.IsAssignableFrom(p.PropertyType)).ToArray();

		    AddPropertiesToDictionary(propertyInfoDictionary, allPropertiesExcludedCommands);

		    AddPropertiesToDictionary(commandsPropertyInfoDictionary, allPropertiesCommands);
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

	    public object GetCommandValue(string commandPropertyName)
	    {
	        var propertyInfo = commandsPropertyInfoDictionary[commandPropertyName];

	        var value = propertyInfo.GetValue(viewModel, null);

	        return value;

        }

        public IEnumerable<PropertyInfo> GetCommands()
        {
            return commandsPropertyInfoDictionary
                .Select(x => x.Value)
                .ToArray();
	    }

	    private void AddPropertiesToDictionary(IDictionary<string, PropertyInfo> dictionary, IEnumerable<PropertyInfo> properties)
	    {
	        foreach (var property in properties)
	        {
	            var propertyName = property.Name;

	            if (dictionary.ContainsKey(propertyName))
	            {
	                const string Message = "The property '{0}' of the ViewModel '{1}' is already declared in a base class.";

	                throw new InvalidOperationException(string.Format(Message, propertyName, currentViewModelType.Name));
	            }

	            dictionary.Add(propertyName, property);
	        }
        }

        private PropertyInfo[] GetAllProperties()
		{
			return
				currentViewModelType
				    .GetProperties(Bindings)
					.Where(p => p.DeclaringType != baseViewModelType)
					.OrderBy(x => x.Name)
				    .ToArray();
		}
	}
}