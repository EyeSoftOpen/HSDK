using System.Collections.Generic;

namespace EyeSoft.Windows.Model.ViewModels.Helpers.Property
{
	internal class PropertyValue
	{
		private readonly string propertyName;

		private readonly INotifyViewModel notifyViewModelViewModel;

		private object value;

		public PropertyValue(string propertyName, INotifyViewModel notifyViewModelViewModel)
		{
			this.propertyName = propertyName;
			this.notifyViewModelViewModel = notifyViewModelViewModel;
		}

		public int Changes { get; private set; }

		public bool IsSetted
		{
			get { return Changes > 0; }
		}

		public object GetValue()
		{
			return value;
		}

		public bool SetValue<T>(T newValue, bool propertyChangedSuspended)
		{
			if (EqualityComparer<object>.Default.Equals(value, newValue))
			{
				return false;
			}

			notifyViewModelViewModel.OnPropertyChanging(propertyName);

			value = newValue;

			if (!propertyChangedSuspended)
			{
				Changes++;
			}

			notifyViewModelViewModel.OnPropertyChanged(propertyName);

			return true;
		}
	}
}