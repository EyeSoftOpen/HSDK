namespace EyeSoft.Windows.Converters
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Windows;

	public class ViewModelToComponentConverter : ValueConverter<object, FrameworkElement>
	{
	    public override FrameworkElement Convert(object value, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			var viewModel = value;
			var viewModelType = viewModel.GetType();

			var uri = Get(viewModelType);

			try
			{
				var component = (FrameworkElement)Application.LoadComponent(uri);
				component.DataContext = value;
				return component;
			}
			catch (Exception exception)
			{
				const string MessageFormat = "The resource with URI '{0}'.";
				var message = string.Format(MessageFormat, uri);
				throw new IOException(message, exception);
			}
		}

		protected virtual Uri Get(Type viewModelType)
		{
			var resourceName = viewModelType.Name.Replace("NavigableViewModel", null).Replace("ViewModel", null);

			return Component.ToUri(resourceName);
		}
	}
}