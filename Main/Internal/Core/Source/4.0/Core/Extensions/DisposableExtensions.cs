namespace EyeSoft.Extensions
{
	using System;

	public static class DisposableExtensions
	{
		public static void Using<T>(this T disposable, Action<T> action) where T : IDisposable
		{
			Enforce
				.Argument(() => disposable)
				.Argument(() => action);

			using (disposable)
			{
				action(disposable);
			}
		}

		public static TRet Using<T, TRet>(this T disposable, Func<T, TRet> func) where T : IDisposable
		{
			Enforce
				.Argument(() => disposable)
				.Argument(() => func);

			using (disposable)
			{
				return func(disposable);
			}
		}
	}
}