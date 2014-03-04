namespace EyeSoft.ServiceModel
{
	public static class OperationContextExtensions
	{
		public static string Username(this IOperationContext operationContext)
		{
			return operationContext.ServiceSecurityContext.PrimaryIdentity.Name;
		}
	}
}