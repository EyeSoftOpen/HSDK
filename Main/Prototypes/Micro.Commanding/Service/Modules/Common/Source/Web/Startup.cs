using System.Web.Http;
using EyeSoft.Accounting.Prototype.Api.Web;
using EyeSoft.Accounting.Prototype.Api.Web.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace EyeSoft.Accounting.Prototype.Api.Web
{
	using System.Web;
	using System.Web.Routing;

	internal class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

			var configuration = new HttpConfiguration();

			configuration.Routes.MapHttpRoute(
				"DefaultApi",
				"{controller}/{action}/{id}",
				new { id = RouteParameter.Optional },
				new { controller = new ControllerConstraint() });

			app
				.UseWebApi(configuration)
				.ConfigureDependencyResolver(configuration)
				.UseSwagger(configuration);
		}
	}
}