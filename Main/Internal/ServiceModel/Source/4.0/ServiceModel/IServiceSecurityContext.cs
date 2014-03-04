namespace EyeSoft.ServiceModel
{
	using System.Security.Principal;

	public interface IServiceSecurityContext
	{
		IIdentity PrimaryIdentity { get; }
	}
}