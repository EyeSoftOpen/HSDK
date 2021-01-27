namespace EyeSoft.Windows.Model.DialogService
{
    using System.Windows;

    public sealed class MessageBox : IMessageBox
	{
		public MessageBoxResult ShowBox(
			Window window,
			string title,
			string message,
			MessageBoxButton button,
			MessageBoxImage icon)
		{
			if (window == null)
			{
				return System.Windows.MessageBox.Show(message, title, button, icon);
			}

			return System.Windows.MessageBox.Show(window, message, title, button, icon);
		}
	}
}