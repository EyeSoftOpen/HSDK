namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using Model;
	using Model.ViewModels.Main.Persisters;

	public class Configuration
	{
		private readonly IWindsorContainer container;

		public Configuration(IWindsorContainer container)
		{
			this.container = container;

			Resolver.Set(new WinsorResolver(container));
		}

		public void Install()
		{
			container.Register(Component.For<IRestClientFactory>().Instance(new RestClientFactory("http://localhost:26741")));

			container.Register(Component.For<DescriptionEstimatePersister>());

			container.Register(Component.For<NameEstimateCustomerPersister>());
		}
	}
}