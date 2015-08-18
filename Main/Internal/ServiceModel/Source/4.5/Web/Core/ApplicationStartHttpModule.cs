namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System.ServiceModel;
	using System.ServiceModel.Description;
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

			httpConfiguration.DependencyResolver = new DependencyResolver(locator);

			httpConfiguration.RemoveXmlFormatterToUseJsonFormatter();

			var routes = RouteTable.Routes;

			var exceptionlessErrorHandlerBehavior = new ExceptionErrorHandlerBehaviorAttribute();

			locator
				.Transient<Calculator>()
				.Transient<IMathService, MathService>();

			var serviceCredentials =
				ServiceBehaviors.ServiceCredentials("EyeSoft", () => new CustomUsernameValidator());

			var binding = new WSHttpBinding(SecurityMode.TransportWithMessageCredential);
			binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;

			var metadata = new ServiceMetadataBehavior { HttpsGetEnabled = true };

			routes.ConfigureService<IMathService, MathService>(new[] { "https" })
				.Behaviors(serviceCredentials, exceptionlessErrorHandlerBehavior, metadata)
				.Bindings(binding);
		}
	}
}