namespace EyeSoft.DynamicProxy
{
	using Castle.DynamicProxy;

	public class PropertyGetInvocation :
		PropertyInvocation
	{
		public PropertyGetInvocation(IInvocation invocation)
			: base(invocation)
		{
		}

		public PropertyGetInvocation Proceed()
		{
			return base.Proceed<PropertyGetInvocation>();
		}
	}
}