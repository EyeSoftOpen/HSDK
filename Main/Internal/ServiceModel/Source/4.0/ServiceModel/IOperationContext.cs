namespace EyeSoft.ServiceModel
{
	public interface IOperationContext
	{
		IServiceSecurityContext ServiceSecurityContext { get; }
	}
}