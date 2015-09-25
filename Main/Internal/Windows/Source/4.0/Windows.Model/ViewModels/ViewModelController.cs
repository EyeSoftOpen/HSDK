using System;
using EyeSoft.Timers;
using EyeSoft.Windows.Model.ServiceProxy;

namespace EyeSoft.Windows.Model.ViewModels
{
	public static class ViewModelController
	{
		private static readonly Singleton<IResolverLocator> singletonContainer =
			new Singleton<IResolverLocator>();

		public static void Set(Func<IResolverLocator> container)
		{
			singletonContainer.Set(container);
		}

		public static ServiceFactory<T> ServiceFactory<T>() where T : IDisposable
		{
			return singletonContainer.Instance.Resolve<ServiceFactory<T>>();
		}

		public static TService Proxy<TService>() where TService : IDisposable
		{
			return singletonContainer.Instance.Resolve<IDisposableFactory<TService>>().Create();
		}

		public static ITimerFactory TimerFactory()
		{
			return Timers.TimerFactory.Create();
		}

		public static TViewModel Create<TViewModel>() where TViewModel : AutoRegisterViewModel
		{
			return singletonContainer.Instance.Resolve<TViewModel>();
		}
	}
}