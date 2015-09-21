namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using EyeSoft.Architecture.Prototype.Windows.Model.Base;
	using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate.Persisters;
	using EyeSoft.Web.Http.Client;

	using Model;

	public class Configuration
	{
		private readonly IWindsorContainer container;

		public Configuration(IWindsorContainer container)
		{
			this.container = container;

			Resolver.Set(new CastleResolver(container));

			BaseApiUrl = "http://localhost:26741";
		}

		public string BaseApiUrl { get; }

		public void Install()
		{
			container.Register(Component.For<IRestClientFactory>().Instance(new RestClientFactory(BaseApiUrl)));

			container.Register(Component.For<DescriptionEstimatePersister>());

			container.Register(Component.For<NameEstimateCustomerPersister>());
		}
	}
}