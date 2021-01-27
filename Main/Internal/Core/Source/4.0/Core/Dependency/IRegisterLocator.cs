namespace EyeSoft.Core
{
    using System;

    public interface IRegisterLocator : IDisposable
	{
		IRegisterLocator Register<TContract>(Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class;

		IRegisterLocator Register<TContract1, TContract2>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class
			where TContract2 : class;

		IRegisterLocator Register<TContract, TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class
			where TImplementation : TContract;

		IRegisterLocator Register<TContract1, TContract2, TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TImplementation : TContract1, TContract2;

		IRegisterLocator Register<TContract1, TContract2, TContract3, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TContract3 : class
			where TImplementation : TContract1, TContract2;

		IRegisterLocator Register<TImplementation>(LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TImplementation : class;

		IRegisterLocator Register<TContract>(TContract instance, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class;

		IRegisterLocator Register<TContract>(Func<TContract> create, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract : class;

		IRegisterLocator Register(Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null);

		IRegisterLocator Register(Type serviceType, Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null);

		IRegisterLocator Register(
			Type serviceType1, Type serviceType2, Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null);

		IRegisterLocator Register(
			Type serviceType1,
			Type serviceType2,
			Type serviceType3,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null);
	}
}