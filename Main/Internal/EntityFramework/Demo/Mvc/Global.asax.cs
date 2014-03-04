namespace EyeSoft.EntityFramework.Caching.Demo.Mvc
{
	using System.Data.Entity.Config;
	using System.Web.Mvc;
	using System.Web.Routing;

	using Castle.Windsor;
	using Castle.Windsor.Installer;

	using EyeSoft.Data.EntityFramework.Caching;
	using EyeSoft.Data.EntityFramework.Toolkit;
	using EyeSoft.EntityFramework.Caching.Demo.Domain;
	using EyeSoft.EntityFramework.Caching.Demo.Mvc.Infratructure;

	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly ICacheWithStatistics cacheWithStatistics = new EntityFrameworkCache();

		private static IWindsorContainer container;

		public static ICacheWithStatistics CacheWithStatistics
		{
			get { return cacheWithStatistics; }
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("favicon.ico");

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				new { controller = "Home", action = "Index", id = UrlParameter.Optional });
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			DbProviderFactoryBase.RegisterProvider<CachingProviderFactory>();
			DbConfiguration.SetConfiguration(new CachedDbConfiguration(CacheWithStatistics));

			using (var context = new NorthwindContext("Northwind"))
			{
				context.Database.CreateIfNotExists();
			}

			BootstrapContainer();

			////new NorthwindContext("Northwind").Database.CreateIfNotExists();
		}

		private static void BootstrapContainer()
		{
			container = new WindsorContainer().Install(FromAssembly.This());
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}
	}
}