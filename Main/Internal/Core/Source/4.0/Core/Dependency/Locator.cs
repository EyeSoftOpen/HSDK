namespace EyeSoft
{
    using System;
    using System.Collections.Generic;

    public static class Locator
	{
		private static readonly Singleton<ILocator> singleton = new Singleton<ILocator>();

		public static ILocator Instance => singleton.Instance;

        public static void Set(ILocator locator)
		{
			singleton.Set(locator);
		}

		public static void Set(Func<ILocator> createLocator)
		{
			singleton.Set(createLocator);
		}

		public static bool IsRegistered(Type serviceType)
		{
			return singleton.Instance.IsRegistered(serviceType);
		}

		public static object Resolve(Type serviceType)
		{
			return singleton.Instance.Resolve(serviceType);
		}

		public static object Resolve(Type serviceType, string key)
		{
			return singleton.Instance.Resolve(serviceType, key);
		}

		public static IEnumerable<object> ResolveAll(Type serviceType)
		{
			return singleton.Instance.ResolveAll(serviceType);
		}

		public static TService Resolve<TService>()
		{
			return singleton.Instance.Resolve<TService>();
		}

		public static TService Resolve<TService>(string key)
		{
			return singleton.Instance.Resolve<TService>(key);
		}

		public static IEnumerable<TService> ResolveAll<TService>()
		{
			return singleton.Instance.ResolveAll<TService>();
		}

		public static object Resolve(Type serviceType, IDictionary<string, object> parameters)
		{
			return singleton.Instance.Resolve(serviceType, parameters);
		}

		public static T Resolve<T>(IDictionary<string, object> parameters)
		{
			return singleton.Instance.Resolve<T>(parameters);
		}

		public static void Release(object obj)
		{
			singleton.Instance.Release(obj);
		}

		public static IRegisterLocator Register<TContract>(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TContract : class
		{
			return singleton.Instance.Register<TContract>(implementationType, lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract1, TContract2>(
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null) where TContract1 : class where TContract2 : class
		{
			return singleton.Instance.Register<TContract1, TContract2>(implementationType, lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TContract : class where TImplementation : TContract
		{
			return singleton.Instance.Register<TContract, TImplementation>(lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract1, TContract2, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null)
			where TContract1 : class where TContract2 : class where TImplementation : TContract1, TContract2
		{
			return singleton.Instance.Register<TContract1, TContract2, TImplementation>(lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract1, TContract2, TContract3, TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null)
			where TContract1 : class where TContract2 : class where TContract3 : class where TImplementation : TContract1, TContract2
		{
			return singleton.Instance.Register<TContract1, TContract2, TContract3, TImplementation>(lifeStyle, key);
		}

		public static IRegisterLocator Register<TImplementation>(
			LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TImplementation : class
		{
			return singleton.Instance.Register<TImplementation>(lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract>(
			TContract instance, LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TContract : class
		{
			return singleton.Instance.Register(instance, lifeStyle, key);
		}

		public static IRegisterLocator Register<TContract>(
			Func<TContract> create, LifeStyle lifeStyle = LifeStyles.Default, string key = null) where TContract : class
		{
			return singleton.Instance.Register(create, lifeStyle, key);
		}

		public static IRegisterLocator Register(
			Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return singleton.Instance.Register(implementationType, lifeStyle, key);
		}

		public static IRegisterLocator Register(
			Type serviceType, Type implementationType, LifeStyle lifeStyle = LifeStyles.Default, string key = null)
		{
			return singleton.Instance.Register(serviceType, implementationType, lifeStyle, key);
		}

		public static IRegisterLocator Register(
			Type serviceType1,
			Type serviceType2,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null)
		{
			return singleton.Instance.Register(serviceType1, serviceType2, implementationType, lifeStyle, key);
		}

		public static IRegisterLocator Register(
			Type serviceType1,
			Type serviceType2,
			Type serviceType3,
			Type implementationType,
			LifeStyle lifeStyle = LifeStyles.Default,
			string key = null)
		{
			return singleton.Instance.Register(serviceType1, serviceType2, serviceType3, implementationType, lifeStyle, key);
		}

		public static void Dispose()
		{
			singleton.Instance.Dispose();
		}
	}
}