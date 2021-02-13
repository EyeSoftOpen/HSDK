namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System.Threading.Tasks;

	using Castle.MicroKernel.Registration;
    using EyeSoft.Messanging;
    using EyeSoft.ServiceLocator;
	using EyeSoft.ServiceLocator.Windsor;
	using EyeSoft.Windows.Model.Input;
    using ViewModels;

    internal static class TestContainer
	{
		public static IDependencyContainer Create()
		{
			var container = new WindsorDependencyContainer();
			var kernel = container.Container;

			kernel.Register(Component.For<ICommandFactory>().ImplementedBy<OnlySyncCommandFactory>());
			kernel.Register(Component.For<CommandsDiscover>());

			kernel.Register(Component.For<IDialogService>().ImplementedBy<TestDefaultDialogService>());
			kernel.Register(Component.For<IMessageBroker>().ImplementedBy<LocalMessageBroker>());
			kernel.Register(Component.For<TaskFactory>());

			kernel.Register(Component.For<MainViewModel>());
			kernel.Register(Component.For<ChildDialogViewModel>());

			return container;
		}
	}
}