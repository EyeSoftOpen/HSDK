namespace EyeSoft
{
    using System;
    using Reflection;

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
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            lock (lockInstance)
            {
                Set(() => instance);
            }
        }

        public void Set(Func<T> valueFactory)
        {
            lock (lockInstance)
            {
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

        internal void Reset(T instance)
        {
            lock (lockInstance)
            {
                Reset(() => instance);
            }
        }

        internal void Reset(Func<T> func)
        {
            lock (lockInstance)
            {
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