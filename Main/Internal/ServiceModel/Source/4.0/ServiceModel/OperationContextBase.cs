namespace EyeSoft.ServiceModel
{
	using EyeSoft;

	public static class OperationContextBase
	{
		private static readonly Singleton<IOperationContext> context =
			new Singleton<IOperationContext>(() => new ServiceOperationContext());

		public static IServiceSecurityContext ServiceSecurityContext
		{
			get { return context.Instance.ServiceSecurityContext; }
		}

		public static void Set(IOperationContext operationContext)
		{
			context.Set(() => operationContext);
		}

		public static string Username()
		{
			return context.Instance.Username();
		}
	}
}