namespace EyeSoft.ServiceLocator
{
	using Microsoft.Practices.ServiceLocation;

	public interface IResolverContainer : IResolverLocator, IServiceLocator
	{
	}
}