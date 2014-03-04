namespace EyeSoft.Web
{
	using System;
	using System.Collections.Generic;
	using System.Web;

	public abstract class BaseHttpModule : IHttpModule
	{
		private static readonly object lockInstance = new object();

		private static readonly ICollection<Type> initializedTypes = new HashSet<Type>();

		public void Init(HttpApplication context)
		{
			lock (lockInstance)
			{
				var type = GetType();

				if (initializedTypes.Contains(type))
				{
					return;
				}

				initializedTypes.Add(type);

				Initiliaze(context);
			}
		}

		public virtual void Dispose()
		{
		}

		protected abstract void Initiliaze(HttpApplication context);
	}
}