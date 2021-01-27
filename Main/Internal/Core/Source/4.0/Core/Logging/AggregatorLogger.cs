namespace EyeSoft.Core.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class AggregatorLogger : IAggregatorLogger
	{
		protected readonly IList<ILogger> loggerList = new List<ILogger>();

		public virtual IAggregatorLogger Register(ILogger logger)
		{
			loggerList.Add(logger);
			return this;
		}

		public virtual void Write(string message)
		{
			foreach (var logger in loggerList)
			{
				logger.Write(message);
			}
		}

		public virtual void Error(Exception exception)
		{
			foreach (var logger in loggerList)
			{
				try
				{
					logger.Error(exception);
				}
				catch
				{
					Debug.WriteLine("Exception occurred logging exception with logger {0}.", logger.GetType().Name);
				}
			}
		}
	}
}