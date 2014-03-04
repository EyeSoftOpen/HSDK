namespace EyeSoft.Windows.Converters
{
	using System;
	using System.Globalization;
	using System.Windows.Data;

	public abstract class ValueConverter<TSource, TDestination> : IValueConverter
	{
		public abstract TDestination Convert(TSource value, CultureInfo culture);

		public virtual TSource ConvertBack(TDestination value, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert((TSource)value, culture);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBack((TDestination)value, culture);
		}
	}
}