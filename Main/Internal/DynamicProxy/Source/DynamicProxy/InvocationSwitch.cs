namespace EyeSoft.DynamicProxy
{
	using System;

	using Castle.DynamicProxy;

	using EyeSoft.DynamicProxy.Interceptors;
	using EyeSoft.Reflection;

	public class InvocationSwitch
	{
		private readonly IInvocation invocation;

		public InvocationSwitch(IInvocation invocation)
		{
			this.invocation = invocation;

			if (invocation.Method.IsPropertySet())
			{
				Operation = Operation.PropertySet;
			}
			else if (invocation.Method.IsPropertyGet())
			{
				Operation = Operation.PropertyGet;
			}
		}

		public Operation Operation { get; private set; }

		public PropertySetInvocation OnPropertySet()
		{
			if (Operation != Operation.PropertySet)
			{
				throw new InvalidOperationException("Can execute actions only for property set.");
			}

			return
				new PropertySetInvocation(invocation);
		}

		public PropertyGetInvocation OnPropertyGet()
		{
			if (Operation != Operation.PropertyGet)
			{
				throw new InvalidOperationException("Can execute actions only for property get.");
			}

			return
				new PropertyGetInvocation(invocation);
		}
	}
}