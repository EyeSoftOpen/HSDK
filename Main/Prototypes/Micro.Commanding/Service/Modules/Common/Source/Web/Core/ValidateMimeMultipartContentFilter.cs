namespace EyeSoft.Accounting.Prototype.Api.Web.Core
{
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using System.Web.Http.Controllers;
	using System.Web.Http.Filters;

	internal class ValidateMimeMultipartContentFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.Request.Content.IsMimeMultipartContent())
			{
				return;
			}

			throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
		}

		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
		}
	}
}