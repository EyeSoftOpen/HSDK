namespace EyeSoft.Wpf.Facilities.Demo.Configuration
{
	using System.Diagnostics;
	using System.Windows;

	using EyeSoft.Windows.Model;

	using MessageBox = System.Windows.MessageBox;

	internal class LoggingMessageBox : IMessageBox
	{
		public MessageBoxResult ShowBox(Window window, string title, string message, MessageBoxButton button, MessageBoxImage icon)
		{
			Debug.WriteLine("Showed MessageBox: Title: {Title} - Message: {Message}".NamedFormat(title, message));

			return MessageBox.Show(window, message, title, button, icon);
		}
	}
}