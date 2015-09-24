namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class FlyoutContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FlyoutViewModel)
            {
                var flyoutView = (ControlTemplate)Application.Current.FindResource("FlyoutView");
                return flyoutView;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}