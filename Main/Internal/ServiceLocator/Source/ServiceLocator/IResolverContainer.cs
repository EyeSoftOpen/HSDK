namespace EyeSoft.ServiceLocator
{
	using System.Web.Mvc;

	using Microsoft.Practices.ServiceLocation;

	public interface IResolverContainer : IResolverLocator, IServiceLocator, IDependencyResolver
	{
	}
}