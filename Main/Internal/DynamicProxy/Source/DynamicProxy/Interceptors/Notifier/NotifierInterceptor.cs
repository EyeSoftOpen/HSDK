namespace EyeSoft.DynamicProxy.Interceptors.Notifier
{
	using Castle.DynamicProxy;

	public class NotifierInterceptor
		: IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			var invocationSwitch =
				new InvocationSwitch(invocation);

			invocation.Proceed();

			switch (invocationSwitch.Operation)
			{
				case Operation.PropertySet:
					invocationSwitch
						.OnPropertySet()
						.RaisePropertyChanged();
					break;
			}
		}
	}
}