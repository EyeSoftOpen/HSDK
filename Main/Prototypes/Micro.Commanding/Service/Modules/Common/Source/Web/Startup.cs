using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
	using System.Web.Http;

	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	using Domain;

	using EyeSoft.Accounting.Prototype.Api.Web.Core;

	using WebApplication1.Controllers;

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

			var configuration = new HttpConfiguration();
			configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{action}/{id}", new { id = RouteParameter.Optional });

			app
				.UseWebApi(configuration)
				.ConfigureDependencyResolver(configuration);
		}
	}
}