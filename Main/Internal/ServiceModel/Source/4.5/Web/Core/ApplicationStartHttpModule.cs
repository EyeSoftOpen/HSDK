namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System;
	using System.Web;
	using System.Web.Http;
	using System.Web.Routing;

	using EyeSoft.ServiceLocator.Windsor;
	using EyeSoft.ServiceModel.Description;
	using EyeSoft.ServiceModel.Registration;
	using EyeSoft.Web;
	using EyeSoft.Web.Http;

	public class ApplicationStartHttpModule : BaseHttpModule
	{
		private static readonly ILocator locator = new WindsorDependencyContainer();

		protected override void Initiliaze(HttpApplication context)
		{
			Locator.Set(locator);

			var httpConfiguration = GlobalConfiguration.Configuration;

			httpConfiguration.DependencyResolver = new HttpLocator(locator);

			httpConfiguration.RemoveXmlFormatterToUseJsonFormatter();

			var routes = RouteTable.Routes;

			var exceptionlessErrorHandlerBehavior = new ExceptionErrorHandlerBehaviorAttribute();

			var serviceCredentials = ServiceBehaviors.ServiceCredentials("EyeMobile", () => new CustomUsernameValidator());

			locator
				.Transient<Calculator>()
				.Transient<IMathService, MathService>();

			routes.ConfigureService<IMathService, MathService>()
				.Behaviors(serviceCredentials, exceptionlessErrorHandlerBehavior)
				.Bindings(Bindings.WsMessageWithUsername.SetClockSkew(TimeSpan.FromMinutes(15)));
		}
	}
}