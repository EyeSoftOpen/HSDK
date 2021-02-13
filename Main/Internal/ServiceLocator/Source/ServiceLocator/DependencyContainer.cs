namespace EyeSoft.ServiceLocator
{
	using System;
    using EyeSoft;

    public abstract class DependencyContainer : ResolverContainer, IDependencyContainer
	{
		protected DependencyContainer(bool throwExceptionOnMissingComponent = true)
			: base(throwExceptionOnMissingComponent)
		{
		}

		public abstract IRegisterLocator Register<TContract>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TContract : class;

		public abstract IRegisterLocator Register<TContract1, TContract2>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class where TContract2 : class;

		public abstract IRegisterLocator Register<TContract, TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class
			where TImplementation : TContract;

		public abstract IRegisterLocator Register<TContract1, TContract2, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TImplementation : TContract1, TContract2;

		public abstract IRegisterLocator Register<TContract1, TContract2, TContract3, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TContract3 : class
			where TImplementation : TContract1, TContract2;

		public abstract IRegisterLocator Register<TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TImplementation : class;

		public abstract IRegisterLocator Register<TContract>(
			TContract instance, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class;

		public abstract IRegisterLocator Register<TContract>(
			Func<TContract> create, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class;

		public abstract IRegisterLocator Register(Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null);

		public abstract IRegisterLocator Register(
			Type serviceType,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null);

		public abstract IRegisterLocator Register(
			Type serviceType1, Type serviceType2, Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null);

		public abstract IRegisterLocator Register(
			Type serviceType1,
			Type serviceType2,
			Type serviceType3,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null);
	}
}