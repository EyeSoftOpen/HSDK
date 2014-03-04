namespace EyeSoft.Windows.Model.Converters
{
	using System;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Windows.Controls;
	using System.Windows.Data;

	public class ErrorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var errors = value as ReadOnlyObservableCollection<ValidationError>;

			if (errors == null)
			{
				return string.Empty;
			}

			return
				errors.Any() ?
					errors[0].ErrorContent :
					string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
