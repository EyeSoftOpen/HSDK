namespace EyeSoft
{
	using System;
	using System.Diagnostics;

	public class Singleton<T> where T : class
	{
		private static readonly Lock<T> lockInstance = new Lock<T>();

		private readonly string typeName = typeof(T).FullName;

		private Lazy<T> singletonInstance;

		private bool used;

		public Singleton()
		{
		}

		public Singleton(Func<T> instance)
		{
			lock (lockInstance)
			{
				Enforce.Argument(() => instance);

				singletonInstance = new Lazy<T>(instance);
			}
		}

		public T Instance
		{
			get
			{
				lock (lockInstance)
				{
					if (singletonInstance == null)
					{
						ThrowExceptionIfNotInitialized();
					}

					var instance = singletonInstance.Value;

					if (!singletonInstance.IsValueCreated)
					{
						ThrowExceptionIfNotInitialized();
					}

					used = true;

					return instance;
				}
			}
		}

		public bool IsValueCreated { get; private set; }

		public void Set(T instance)
		{
			lock (lockInstance)
			{
				Enforce.Argument(() => instance);

				Set(() => instance);
			}
		}

		public void Set(Func<T> valueFactory)
		{
			lock (lockInstance)
			{
				Enforce.Argument(() => valueFactory);

				if (used)
				{
					var message = string.Format("The singleton instance of type {0} can be changed only before any usage.", typeName);
					throw new InvalidOperationException(message);
				}

				if (IsValueCreated || ((singletonInstance != null) && singletonInstance.IsValueCreated))
				{
					var message = string.Format("The singleton instance of type {0} can be assigned only one time.", typeName);
					throw new InvalidOperationException(message);
				}

				singletonInstance = new Lazy<T>(valueFactory);

				IsValueCreated = true;
			}
		}

		public void Reset(T instance)
		{
			lock (lockInstance)
			{
				Enforce.Argument(() => instance);

				Reset(() => instance);
			}
		}

		public void Reset(Func<T> func)
		{
			lock (lockInstance)
			{
				Enforce.Argument(() => func);

				singletonInstance = new Lazy<T>(func);
			}
		}

		private void ThrowExceptionIfNotInitialized()
		{
			var message = string.Format("The singleton instance of type {0} was not initialized.", typeName);
			throw new InvalidOperationException(message);
		}
	}
}