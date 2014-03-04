namespace EyeSoft.ServiceLocator.Windsor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Castle.MicroKernel.Registration;
	using Castle.Windsor;

	public class WindsorDependencyContainer : DependencyContainer
	{
		protected readonly IWindsorContainer container;

		public WindsorDependencyContainer(bool throwExceptionOnMissingComponent = true)
			: this(new WindsorContainer(), throwExceptionOnMissingComponent)
		{
		}

		public WindsorDependencyContainer(IWindsorContainer container, bool throwExceptionOnMissingComponent = true)
			: base(throwExceptionOnMissingComponent)
		{
			this.container = container;
		}

		public IWindsorContainer Container
		{
			get { return container; }
		}

		public override bool IsRegistered(Type serviceType)
		{
			var isRegistered = container.Kernel.GetAssignableHandlers(serviceType).Any();

			return isRegistered;
		}

		public override object Resolve(Type serviceType, IDictionary<string, object> parameters)
		{
			return TryResolve(() => container.Resolve(serviceType, new Dictionary<string, object>(parameters)));
		}

		public override T Resolve<T>(IDictionary<string, object> parameters)
		{
			return TryResolve(() => container.Resolve<T>(new Dictionary<string, object>(parameters)));
		}

		public override object Resolve(Type serviceType)
		{
			return TryResolve(() => container.Resolve(serviceType));
		}

		public override object Resolve(Type serviceType, string key)
		{
			return TryResolve(() => container.Resolve(serviceType, key));
		}

		public override IEnumerable<object> ResolveAll(Type serviceType)
		{
			return TryResolve(() => container.ResolveAll(serviceType).Cast<object>());
		}

		public override TService Resolve<TService>()
		{
			return TryResolve(() => container.Resolve<TService>());
		}

		public override TService Resolve<TService>(string key)
		{
			return TryResolve(() => container.Resolve<TService>(key));
		}

		public override IEnumerable<TService> ResolveAll<TService>()
		{
			return TryResolve(() => container.ResolveAll<TService>());
		}

		public override void Release(object instance)
		{
			container.Release(instance);
		}

		public override IRegisterLocator Register<TContract>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract>().ImplementedBy(implementationType).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract1, TContract2>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract1, TContract2>().ImplementedBy(implementationType).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract, TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract>().ImplementedBy<TImplementation>().Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract1, TContract2, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract1, TContract2>().ImplementedBy<TImplementation>().Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract1, TContract2, TContract3, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract1, TContract2, TContract3>().ImplementedBy<TImplementation>().Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TImplementation>().Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract>(
			TContract instance, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract>().Instance(instance).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register<TContract>(
			Func<TContract> func, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For<TContract>().UsingFactoryMethod(func).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register(Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For(implementationType).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register(
			Type serviceType,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null)
		{
			return Component.For(serviceType, implementationType).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register(
			Type serviceType1, Type serviceType2, Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return Component.For(serviceType1, serviceType2, implementationType).Include(this, lifeStyle, key);
		}

		public override IRegisterLocator Register(
			Type serviceType1,
			Type serviceType2,
			Type serviceType3,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null)
		{
			return Component.For(serviceType1, serviceType2, serviceType3, implementationType).Include(this, lifeStyle, key);
		}

		public override void Dispose()
		{
			container.Dispose();
		}

		protected override object DoGetInstance(Type serviceType, string key)
		{
			return key != null ? container.Resolve(key, serviceType) : container.Resolve(serviceType);
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			return (object[])container.ResolveAll(serviceType);
		}

		private T TryResolve<T>(Func<T> func)
		{
			try
			{
				return func();
			}
			catch (Exception e)
			{
				throw new ComponentResolutionException(e);
			}
		}
	}
}