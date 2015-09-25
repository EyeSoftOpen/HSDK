using System;
using System.Collections.Generic;
using System.Reflection;
using EyeSoft.Collections.Concurrent;
using EyeSoft.Windows.Model.ViewModels.Helpers.Property.FluentInterface;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property
{
	internal class ViewModelProperties
	{
		private readonly INotifyViewModel notifyViewModelViewModel;

		private readonly IDictionary<string, ViewModelProperty> viewModelProperties =
			new SafeConcurrentDictionary<string, ViewModelProperty>();

		private readonly IDictionary<string, List<string>> propertiesDependencies =
			new SafeConcurrentDictionary<string, List<string>>();

		public ViewModelProperties(INotifyViewModel notifyViewModelViewModel)
		{
			this.notifyViewModelViewModel = notifyViewModelViewModel;
		}

		public int Changes { get; private set; }

		public void Changing(PropertyInfo propertyInfo, Func<object> valueFactory)
		{
			var viewModelProperty = ViewModelProperty(propertyInfo);

			viewModelProperty.Changing(valueFactory);
		}

		public void Changed(PropertyInfo propertyInfo, Func<object> valueFactory)
		{
			var propertyName = propertyInfo.Name;

			var viewModelProperty = ViewModelProperty(propertyInfo);

			viewModelProperty.Changed(valueFactory);

			RefreshDependentProperties(propertyName);
		}

		public T GetProperty<T>(PropertyInfo propertyInfo)
		{
			return ViewModelProperty(propertyInfo).GetValue<T>();
		}

		public void SetProperty<T>(PropertyInfo propertyInfo, T value, bool propertyChangedSuspended)
		{
			var wasDifferentFromOriginal = ViewModelProperty(propertyInfo).SetValue(value, propertyChangedSuspended);

			if (propertyChangedSuspended)
			{
				return;
			}

			if (wasDifferentFromOriginal)
			{
				Changes++;
			}
		}

		public IViewModelProperty<TProperty> Property<TProperty>(PropertyInfo propertyInfo)
		{
			return ViewModelProperty<TProperty>(propertyInfo);
		}

		private ViewModelProperty<TProperty> ViewModelProperty<TProperty>(PropertyInfo propertyInfo)
		{
			return (ViewModelProperty<TProperty>)ViewModelProperty(propertyInfo);
		}

		private ViewModelProperty ViewModelProperty(PropertyInfo propertyInfo)
		{
			var propertyName = propertyInfo.Name;

			if (!viewModelProperties.ContainsKey(propertyName))
			{
				var dependentProperty =
					Helpers.Property.ViewModelProperty.Create(propertyInfo, notifyViewModelViewModel, AddDependencies);

				viewModelProperties.Add(propertyName, dependentProperty);
			}

			var viewModelProperty = viewModelProperties[propertyName];

			return viewModelProperty;
		}

		private void RefreshDependentProperties(string propertyName)
		{
			if (!propertiesDependencies.ContainsKey(propertyName))
			{
				return;
			}

			foreach (var dependency in propertiesDependencies[propertyName])
			{
				notifyViewModelViewModel.OnPropertyChanged(dependency);
			}
		}

		private void AddDependencies(string propertyName, string[] dependencies)
		{
			foreach (var dependency in dependencies)
			{
				if (!propertiesDependencies.ContainsKey(dependency))
				{
					propertiesDependencies.Add(dependency, new List<string>());
				}

				propertiesDependencies[dependency].Add(propertyName);
			}
		}
	}
}