using System;
using System.Reflection;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property
{
	internal class ViewModelProperty
	{
		protected readonly PropertyChanges changedActions = new PropertyChanges();

		protected readonly PropertyChanges changingActions = new PropertyChanges();

		private static readonly Type viewModelPropertyType = typeof(ViewModelProperty<>);

		private readonly PropertyValue propertyValue;

		protected ViewModelProperty(string propertyName, INotifyViewModel notifyViewModelViewModel)
		{
			PropertyName = propertyName;

			propertyValue = new PropertyValue(PropertyName, notifyViewModelViewModel);
		}

		public string PropertyName { get; private set; }

		public static ViewModelProperty Create(
			PropertyInfo propertyInfo,
			INotifyViewModel notifyViewModelViewModel,
			Action<string, string[]> addDependencies)
		{
			var viewModelPropertyTypeGeneric = viewModelPropertyType.MakeGenericType(propertyInfo.PropertyType);
			var arguments = new object[] { notifyViewModelViewModel, addDependencies, propertyInfo.Name };

			var viewModelPropertyGenericInstance = Activator.CreateInstance(viewModelPropertyTypeGeneric, arguments);

			return (ViewModelProperty)viewModelPropertyGenericInstance;
		}

		public TProperty GetValue<TProperty>()
		{
			var value = propertyValue.GetValue();

			var valueTyped = value == null ? default(TProperty) : (TProperty)value;

			return valueTyped;
		}

		public bool SetValue<TProperty>(TProperty value, bool propertyChangedSuspended)
		{
			return propertyValue.SetValue(value, propertyChangedSuspended);
		}

		public void Changing(Func<object> valueFactory)
		{
			var value = GetValue(valueFactory);

			changingActions.PropertySetted(value);
		}

		public void Changed(Func<object> valueFactory)
		{
			var value = propertyValue.IsSetted ? propertyValue.GetValue() : valueFactory();

			changedActions.PropertySetted(value);
		}

		private object GetValue(Func<object> valueFactory)
		{
			var value = propertyValue.IsSetted ? propertyValue.GetValue() : valueFactory();
			return value;
		}
	}
}