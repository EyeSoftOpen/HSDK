namespace EyeSoft.Extensions
{
    using System;

    public static class DisposableExtensions
    {
        public static void Using<T>(this T disposable, Action<T> action) where T : IDisposable
        {
            using (disposable)
            {
                action(disposable);
            }
        }

        public static TRet Using<T, TRet>(this T disposable, Func<T, TRet> func) where T : IDisposable
        {
            using (disposable)
            {
                return func(disposable);
            }
        }
    }
}