namespace EyeSoft.EntityFramework.Caching.Demo.Mvc.Infratructure
{
	using System;
	using System.Configuration;

	using Castle.MicroKernel.Registration;
	using Castle.MicroKernel.SubSystems.Configuration;
	using Castle.Windsor;

	using EyeSoft.EntityFramework.Caching.Demo.Domain;

	/// <summary>
	/// Registers the services used by application with the container
	/// </summary>
	public class ServiceInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

			if (string.IsNullOrEmpty(connectionString))
			{
				throw new Exception(
					"The connection string for Northwind could not be found in the configuration, please make sure you have set this");
			}

			container.Register(
				Component.For<IDbContext>()
					.ImplementedBy<NorthwindContext>()
					.LifeStyle.PerWebRequest
					.Named("NorthwindCachedContext")
					.DependsOn(Parameter.ForKey("nameOrConnectionString").Eq(connectionString)));
		}
	}
}