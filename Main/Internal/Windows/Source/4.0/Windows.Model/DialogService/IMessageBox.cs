using System.Windows;

namespace EyeSoft.Windows.Model.DialogService
{
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