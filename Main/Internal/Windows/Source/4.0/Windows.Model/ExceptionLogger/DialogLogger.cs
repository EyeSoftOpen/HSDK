namespace EyeSoft.Windows.Model.ExceptionLogger
{
    using System;
    using System.Windows;
    using Core.Extensions;
    using Core.Logging;
    using DialogService;

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