namespace EyeSoft
{
	using System;

	public static class RegisterLocatorExtensions
	{
		public static IRegisterLocator Singleton<TContract>(this IRegisterLocator locator, Type implementationType, string key = null)
			where TContract : class
		{
			locator.Register<TContract>(implementationType, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton<TContract1, TContract2>(
			this IRegisterLocator locator, Type implementationType, string key = null)
			where TContract1 : class
			where TContract2 : class
		{
			locator.Register<TContract1, TContract2>(implementationType, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton<TContract, TImplementation>(this IRegisterLocator locator, string key = null)
			where TContract : class
			where TImplementation : TContract
		{
			locator.Register<TContract, TImplementation>(LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton<TContract1, TContract2, TImplementation>(this IRegisterLocator locator, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TImplementation : TContract1, TContract2
		{
			locator.Register<TContract1, TContract2, TImplementation>(LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton<TImplementation>(
			this IRegisterLocator locator, string key = null) where TImplementation : class
		{
			locator.Register<TImplementation>(LifeStyle.Singleton);
			return locator;
		}

		public static IRegisterLocator Singleton<TImplementation>(
			this IRegisterLocator locator, TImplementation instance, string key = null)
			where TImplementation : class
		{
			locator.Register(instance, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton<T>(this IRegisterLocator locator, Func<T> func, string key = null) where T : class
		{
			locator.Register(func, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton(this IRegisterLocator locator, Type seviceType, Type implementationType, string key = null)
		{
			locator.Register(seviceType, implementationType, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Singleton(this IRegisterLocator locator, Type implementationType, string key = null)
		{
			locator.Register(implementationType, LifeStyle.Singleton, key);
			return locator;
		}

		public static IRegisterLocator Transient<TContract>(this IRegisterLocator locator, Type implementationType, string key = null)
			where TContract : class
		{
			locator.Register<TContract>(implementationType, LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<TContract1, TContract2>(
			this IRegisterLocator locator, Type implementationType, string key = null)
			where TContract1 : class
			where TContract2 : class
		{
			locator.Register<TContract1, TContract2>(implementationType, LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<TContract, TImplementation>(this IRegisterLocator locator, string key = null)
			where TContract : class
			where TImplementation : TContract
		{
			locator.Register<TContract, TImplementation>(LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<TContract1, TContract2, TImplementation>(this IRegisterLocator locator, string key = null)
			where TContract1 : class
			where TContract2 : class
			where TImplementation : TContract1, TContract2
		{
			locator.Register<TContract1, TContract2, TImplementation>(LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<TImplementation>(
			this IRegisterLocator locator, string key = null) where TImplementation : class
		{
			locator.Register<TImplementation>(LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<TImplementation>(
			this IRegisterLocator locator, TImplementation instance, string key = null)
			where TImplementation : class
		{
			locator.Register(instance, LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient<T>(this IRegisterLocator locator, Func<T> func, string key = null)
			where T : class
		{
			locator.Register(func, LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient(this IRegisterLocator locator, Type seviceType, Type implementationType, string key = null)
		{
			locator.Register(seviceType, implementationType, LifeStyle.Transient, key);
			return locator;
		}

		public static IRegisterLocator Transient(this IRegisterLocator locator, Type implementationType, string key = null)
		{
			locator.Register(implementationType, LifeStyle.Transient, key);
			return locator;
		}
	}
}