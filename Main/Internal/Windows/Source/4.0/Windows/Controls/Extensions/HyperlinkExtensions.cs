namespace EyeSoft.Windows.Controls
{
	using System.Diagnostics;
	using System.Windows;
	using System.Windows.Documents;

	public static class HyperlinkExtensions
	{
		public static readonly DependencyProperty IsExternalProperty =
			DependencyProperty.RegisterAttached("IsExternal", typeof(bool), typeof(HyperlinkExtensions), new UIPropertyMetadata(false, OnIsExternalChanged));

		public static bool GetIsExternal(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsExternalProperty);
		}

		public static void SetIsExternal(DependencyObject obj, bool value)
		{
			obj.SetValue(IsExternalProperty, value);
		}

		private static void OnIsExternalChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			var hyperlink = sender as Hyperlink;

			if ((bool)args.NewValue)
			{
				hyperlink.RequestNavigate += HyperlinkRequestNavigate;
			}
			else
			{
				hyperlink.RequestNavigate -= HyperlinkRequestNavigate;
			}
		}

		private static void HyperlinkRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}
	}
}