﻿namespace EyeSoft.Architecture.Prototype.Windows
{
	using System;
	using Castle.Windsor;
	using EyeSoft.Architecture.Prototype.Windows.Core;
	using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels;
	using EyeSoft.Logging;
	using EyeSoft.Threading.Tasks;
	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.DialogService;

	public partial class App
	{
		public App()
		{
			try
			{
				var logger = new ApplicationLogger();

				Logger.Register(logger);

				this.InstallExceptionHandler(new TestExceptionHandler(this, logger, false));

				ThreadingFactory.Set(new ApplicationTaskScheduler(logger));

				var container = new WindsorContainer();

				new Configuration.Configuration(container).Install();

				DialogService.ShowMain<MainViewModel>();
			}
			catch (Exception exception)
			{
				Logger.Error(exception);
			}
		}
	}
}