namespace EyeSoft.DynamicProxy
{
	using System;

	using Castle.DynamicProxy;

	using EyeSoft.Reflection;

	public abstract class PropertyInvocation
	{
		private readonly IInvocation invocation;

		protected PropertyInvocation(IInvocation invocation)
		{
			this.invocation = invocation;
			TargetType = invocation.TargetType;
			Instance = invocation.InvocationTarget;
			PropertyName = invocation.Method.PropertyName();
			IsSet = invocation.Method.IsPropertySet();
			IsGet = invocation.Method.IsPropertyGet();

			if ((!IsSet) && (!IsGet))
			{
				throw new ArgumentException("The operation must be a property get/set.");
			}
		}

		public bool IsGet { get; private set; }

		public bool IsSet { get; private set; }

		public Type TargetType { get; private set; }

		public object Instance { get; private set; }

		public string PropertyName { get; private set; }

		protected T Proceed<T>()
			where T : PropertyInvocation
		{
			invocation.Proceed();
			return (T)this;
		}
	}
}