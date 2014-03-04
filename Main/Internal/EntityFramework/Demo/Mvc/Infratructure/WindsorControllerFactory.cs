namespace EyeSoft.EntityFramework.Caching.Demo.Mvc.Infratructure
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	using Castle.MicroKernel;

	public class WindsorControllerFactory : DefaultControllerFactory
	{
		private readonly IKernel kernel;

		public WindsorControllerFactory(IKernel kernel)
		{
			this.kernel = kernel;
		}

		public override void ReleaseController(IController controller)
		{
			kernel.ReleaseComponent(controller);
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				var message = string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path);
				throw new HttpException(404, message);
			}

			return (IController)kernel.Resolve(controllerType);
		}
	}
}