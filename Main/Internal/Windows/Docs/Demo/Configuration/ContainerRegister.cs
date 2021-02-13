namespace EyeSoft.Windows.Model.Demo.Configuration
{
    using EyeSoft;
    using EyeSoft.Mapping;
    using EyeSoft.Messanging;
    using EyeSoft.Timers;
    using EyeSoft.AutoMapper;
    using EyeSoft.ServiceLocator.Windsor;
    using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Input;
	using EyeSoft.Windows.Model.Threading;

    public static class ContainerRegister
	{
		private static readonly ILocator container = new WindsorDependencyContainer();

		static ContainerRegister()
		{
			RegisterMultiThreadConfiguration(true);

			container
				.Singleton(container)
				.Singleton<ICommandFactory, CommandFactory>()
				.Singleton(() => new CommandsDiscover(EvaluateExecuteCommandFactoryHalper.Create(true)))
				//.Singleton(() => new CommandsDiscover(AlwaysCanExecuteCommandFactoryHalper.Create(true, new DefaultViewModelChecker("Validation errors on UI", "Check the UI"))))
				.Singleton<IMessageBroker, LocalMessageBroker>()
				.Singleton<IDialogService, DefaultDialogService>()
				.Singleton<IMapper, AutoMapperMapper>()
				.Singleton(typeof(ServiceFactory<>))
				.Singleton<IMessageBox, LoggingMessageBox>()
				.Singleton(typeof(IDisposableFactory<>), typeof(DemoProxyFactory<>))
				.Singleton<CommandConvention, DemoCommandConvention>();

			TimerFactory.Set(new DispatcherTimerFactory());
			CollectionFactory.Set(() => new SafeCollectionFactory());
			CommandsDiscoverFactory.Set(container.Resolve<CommandsDiscover>);
			MessageBroker.Set(container.Resolve<IMessageBroker>);
			DialogService.Set(container.Resolve<IDialogService>);
			Mapper.Set(container.Resolve<IMapper>);
			ViewModelController.Set(() => container);
		}

		public static void Initialize()
		{
		}

		private static void RegisterMultiThreadConfiguration(bool multiThread)
		{
			WindowsThreadingFactory.SetMode(multiThread);
		}
	}
}