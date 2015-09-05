namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	using System;

	using Castle.Windsor;

	using Model;

	internal class WinsorResolver : IResolver
	{
		private readonly IWindsorContainer container;

		public WinsorResolver(IWindsorContainer container)
		{
			this.container = container;
		}

		public T Resolve<T>(Type type)
		{
			return (T)container.Resolve(type);
		}

		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}
	}
}