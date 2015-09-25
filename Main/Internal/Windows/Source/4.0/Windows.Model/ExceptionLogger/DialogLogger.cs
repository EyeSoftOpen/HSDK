using System;
using System.Windows;
using EyeSoft.Logging;

namespace EyeSoft.Windows.Model.ExceptionLogger
{
	public class DialogLogger : ILogger
	{
		public void Write(string message)
		{
		}

		public void Error(Exception exception)
		{
			Application.Current.Sync()
				.Execute(() => DialogService.DialogService.ShowMessage("Error", exception.Format(), MessageBoxButton.OK, MessageBoxImage.Error));
		}
	}
}