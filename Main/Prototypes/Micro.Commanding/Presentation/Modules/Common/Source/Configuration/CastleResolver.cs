using EyeSoft.Architecture.Prototype.Windows.Model.Base;

namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	using System;

	using Castle.Windsor;

	using Model;

	internal class CastleResolver : IResolver
	{
		private readonly IWindsorContainer container;

		public CastleResolver(IWindsorContainer container)
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