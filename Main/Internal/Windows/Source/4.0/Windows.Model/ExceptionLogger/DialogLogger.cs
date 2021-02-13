namespace EyeSoft.Windows.Model
{
	using System;
	using System.Windows;
    using Extensions;
    using EyeSoft.Logging;

	public class DialogLogger : ILogger
	{
		public void Write(string message)
		{
		}

		public void Error(Exception exception)
		{
			Application.Current.Sync()
				.Execute(() => DialogService.ShowMessage("Error", exception.Format(), MessageBoxButton.OK, MessageBoxImage.Error));
		}
	}
}