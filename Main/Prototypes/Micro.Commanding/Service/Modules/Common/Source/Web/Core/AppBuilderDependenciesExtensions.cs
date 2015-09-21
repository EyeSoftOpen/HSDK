namespace EyeSoft.Accounting.Prototype.Api.Web.Core
{
	using System.Data.SqlClient;
	using System.Web.Http;

	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using Domain;

	using EyeSoft.Accounting.Prototype.Api.Web.Controllers;

	using Owin;

	internal static class AppBuilderDependenciesExtensions
	{
		public static IAppBuilder ConfigureDependencyResolver(this IAppBuilder appBuilder, HttpConfiguration configuration)
		{
			var container = new WindsorContainer();

			container
				.Register(Component.For<FinancialDbContext>().UsingFactoryMethod(() => new FinancialDbContext(new SqlConnectionStringBuilder { DataSource = ".", InitialCatalog = "Es.Prototype", IntegratedSecurity = true })).LifestyleTransient())
				.Register(Component.For<IEstimateRepository, EstimateRepository>().LifestyleTransient())
				.Register(Component.For<EstimateController>().LifestyleTransient());

			configuration.DependencyResolver = new CastleResolver(container);

			return appBuilder;
		}
	}
}