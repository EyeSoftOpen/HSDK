namespace EyeSoft.Diagnostic
{
    using System;

    public static class SystemInspector
	{
		public static class Debugger
		{
			private static readonly Singleton<IDebugger> singletonInstance =
				new Singleton<IDebugger>(() => new DefaultDebugger());

			public static bool IsAttached => singletonInstance.Instance.IsAttached;

            public static void SetAsDetached()
			{
				Set(false);
			}

			public static void SetAsAttached()
			{
				Set(true);
			}

			public static void Set(bool isAttached)
			{
				Set(() => new DefaultDebugger(isAttached));
			}

			public static void Set(Func<IDebugger> instance)
			{
				singletonInstance.Set(instance);
			}

			public static bool IsLogging()
			{
				return singletonInstance.Instance.IsLogging();
			}
		}
	}
}