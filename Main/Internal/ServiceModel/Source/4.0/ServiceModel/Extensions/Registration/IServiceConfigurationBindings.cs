namespace EyeSoft.ServiceModel.Registration
{
	using System.Collections.Generic;
	using System.ServiceModel.Channels;
	using System.Web.Routing;

	public interface IServiceConfigurationBindings
	{
		ICollection<RouteBase> Bindings(params Binding[] bindings);
	}
}