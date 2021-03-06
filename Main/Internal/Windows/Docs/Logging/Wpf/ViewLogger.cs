﻿namespace EyeSoft.Docs.Logging.Windows
{
    using System;
    using EyeSoft.Extensions;
    using EyeSoft.Logging;
    using ViewModels;

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