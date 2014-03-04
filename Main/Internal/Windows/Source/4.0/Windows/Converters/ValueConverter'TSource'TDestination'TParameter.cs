namespace EyeSoft.Windows.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public abstract class ValueConverter<TSource, TDestination, TParameter> : IValueConverter
	{
		public abstract TDestination Convert(TSource value, TParameter parameter, CultureInfo culture);

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var defaultValue = default(TSource);

			if (Equals(defaultValue, value))
			{
				return defaultValue;
			}

			var sourceType = typeof(TSource);
			var valueType = value.GetType();

			if (sourceType != valueType)
			{
				var message =
					string.Format("Expected value of type {0} but received type {1}.", sourceType.Name, valueType.Name);

				throw new ArgumentException(message);
			}

			return Convert((TSource)value, (TParameter)parameter, culture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}