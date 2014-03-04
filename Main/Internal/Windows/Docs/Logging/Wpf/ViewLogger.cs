namespace EyeSoft.Docs.Logging.Wpf
{
	using System;

	using EyeSoft.Docs.Logging.Wpf.ViewModels;
	using EyeSoft.Logging;

	internal class ViewLogger : ILogger
	{
		private readonly MainViewModel mainViewModel;

		public ViewLogger(MainViewModel mainViewModel)
		{
			this.mainViewModel = mainViewModel;
		}

		public void Write(string message)
		{
			throw new NotImplementedException();
		}

		public void Error(Exception exception)
		{
			mainViewModel.LogError(exception.Format(), DateTime.Now);
		}
	}
}