namespace EyeSoft.Core.Logging
{
    using System;
    using System.Diagnostics;

    public static class Logger
	{
		private static readonly Singleton<IAggregatorLogger> agregatorLogger =
			new Singleton<IAggregatorLogger>(() => new DefaultAggregatorLogger());

		public static ILogger Instance => agregatorLogger.Instance;

        public static void Set(IAggregatorLogger instance)
		{
			agregatorLogger.Set(instance);
		}

		public static void Set(Func<IAggregatorLogger> func)
		{
			agregatorLogger.Set(func);
		}

		[Conditional("DEBUG")]
		public static void Reset(IAggregatorLogger instance)
		{
			agregatorLogger.Reset(instance);
		}

		public static IAggregatorLogger Register(ILogger logger)
		{
			return agregatorLogger.Instance.Register(logger);
		}

		////[Conditional("DEBUG")]
		public static void Write(string message)
		{
			agregatorLogger.Instance.Write(message);
		}

		public static void Error(Exception exception)
		{
			agregatorLogger.Instance.Error(exception);
		}
	}
}