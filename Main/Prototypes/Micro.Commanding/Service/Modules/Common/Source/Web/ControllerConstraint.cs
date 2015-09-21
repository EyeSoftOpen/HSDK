namespace EyeSoft.Accounting.Prototype.Api.Web
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Web.Http.Routing;

	internal class ControllerConstraint : IHttpRouteConstraint
	{
		public bool Match(
			HttpRequestMessage request,
			IHttpRoute route,
			string parameterName,
			IDictionary<string, object> values,
			HttpRouteDirection routeDirection)
		{
			var isSwagger = "swagger".Equals(values["controller"] as string, StringComparison.InvariantCultureIgnoreCase);

			return !isSwagger;
		}
	}
}