namespace EyeSoft.ServiceModel.Registration
{
	using System.Collections.Generic;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.Web.Routing;

	public interface IServiceConfiguration
	{
		IServiceConfigurationBehaviors Behaviors(params IServiceBehavior[] behaviors);

		ICollection<RouteBase> Bindings(params Binding[] bindings);
	}
}