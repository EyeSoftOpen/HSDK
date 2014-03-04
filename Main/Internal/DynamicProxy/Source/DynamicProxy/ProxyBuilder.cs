namespace EyeSoft.DynamicProxy
{
	using Castle.DynamicProxy;

	public class ProxyBuilder
	{
		private readonly ProxyGenerator proxyGenenerator = new ProxyGenerator();

		public ProxyBuilder()
		{
			ProxyGenerator = proxyGenenerator;
		}

		internal ProxyGenerator ProxyGenerator { get; private set; }
	}
}