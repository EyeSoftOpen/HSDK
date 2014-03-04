namespace EyeSoft.DynamicProxy
{
	using Castle.DynamicProxy;

	public class PropertySetInvocation :
		PropertyInvocation
	{
		public PropertySetInvocation(IInvocation invocation)
			: base(invocation)
		{
		}

		public PropertySetInvocation Proceed()
		{
			return Proceed<PropertySetInvocation>();
		}
	}
}