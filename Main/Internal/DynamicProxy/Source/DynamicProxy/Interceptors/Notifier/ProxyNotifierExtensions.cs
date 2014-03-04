namespace EyeSoft.DynamicProxy.Interceptors.Notifier
{
	using System.ComponentModel;

	public static class ProxyNotifierExtensions
	{
		public static T MakeNotifyPropertyChanged<T>(this ProxyBuilder proxy)
			where T : class, new()
		{
			return
				new FluentProxy<T>(proxy)
					.Implements<INotifyPropertyChanged>()
					.Interceptor<NotifierInterceptor>()
					.Create();
		}
	}
}