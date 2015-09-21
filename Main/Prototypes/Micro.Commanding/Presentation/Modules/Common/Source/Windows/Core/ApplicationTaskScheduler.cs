using System;
using EyeSoft.Logging;

namespace EyeSoft.Architecture.Prototype.Windows.Core
{
	public class ApplicationTaskScheduler : EyeSoft.Threading.Tasks.Schedulers.LimitedConcurrencyLevelTaskScheduler
	{
		private readonly ILogger logger;

		public ApplicationTaskScheduler(ILogger logger)
		{
			this.logger = logger;
		}

		protected override void OnTaskFault(AggregateException exception)
		{
			logger.Error(exception);
			base.OnTaskFault(exception);
		}
	}
}