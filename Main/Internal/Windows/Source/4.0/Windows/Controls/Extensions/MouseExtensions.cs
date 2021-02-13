namespace EyeSoft.Windows.Controls
{
	using System.Windows;
	using System.Windows.Input;

	public class MouseExtensions
	{
		public static readonly DependencyProperty MouseUpCommandProperty =
			DependencyProperty
				.RegisterAttached(
					"MouseUpCommand",
					typeof(ICommand),
					typeof(MouseExtensions),
					new FrameworkPropertyMetadata(MouseUpCommandChanged));

		public static void SetMouseUpCommand(UIElement element, ICommand value)
		{
			element.SetValue(MouseUpCommandProperty, value);
		}

		private static ICommand GetMouseUpCommand(DependencyObject element)
		{
			return (ICommand)element.GetValue(MouseUpCommandProperty);
		}

		private static void MouseUp(object sender, MouseButtonEventArgs e)
		{
			var element = (FrameworkElement)sender;

			var command = GetMouseUpCommand(element);

			command.Execute(e);
		}

		private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var element = (FrameworkElement)d;

			element.MouseUp += MouseUp;
		}
	}
}