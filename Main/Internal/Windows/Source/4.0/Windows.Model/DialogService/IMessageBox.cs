namespace EyeSoft.Windows.Model
{
	using System.Windows;

	public interface IMessageBox
	{
		MessageBoxResult ShowBox(
			Window window,
			string title,
			string message,
			MessageBoxButton button,
			MessageBoxImage icon);
	}
}