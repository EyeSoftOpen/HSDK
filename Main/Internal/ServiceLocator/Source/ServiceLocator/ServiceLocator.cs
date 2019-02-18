namespace EyeSoft.ServiceLocator
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using CommonServiceLocator;

	public abstract class ServiceLocator : ServiceLocatorImplBase, IServiceLocator
	{
		private readonly bool throwExceptionOnMissingComponent;

		protected ServiceLocator(bool throwExceptionOnMissingComponent = true)
		{
			this.throwExceptionOnMissingComponent = throwExceptionOnMissingComponent;
		}

		object IServiceLocator.GetInstance(Type serviceType)
		{
			return base.GetInstance(serviceType);
		}

		public override object GetInstance(Type serviceType)
		{
			if (!throwExceptionOnMissingComponent)
			{
				if (!IsRegistered(serviceType))
				{
					return null;
				}
			}

			return base.GetInstance(serviceType);
		}

		public override IEnumerable<TService> GetAllInstances<TService>()
		{
			if (!throwExceptionOnMissingComponent)
			{
				var serviceType = typeof(TService);

				if (!IsRegistered(serviceType))
				{
					return Enumerable.Empty<TService>();
				}
			}

			return base.GetAllInstances<TService>();
		}

		public abstract bool IsRegistered(Type serviceType);
	}
}