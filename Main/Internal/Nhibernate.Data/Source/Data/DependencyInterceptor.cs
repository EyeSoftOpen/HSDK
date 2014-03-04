namespace EyeSoft.Data.Nhibernate
{
	using System;
	using System.Collections.Generic;

	using Microsoft.Practices.ServiceLocation;

	using NHibernate;

	public class DependencyInterceptor
		: EmptyInterceptor
	{
		private readonly IDictionary<string, Type> typesDictionary;

		private readonly Action<IComparable, object> setKey;

		private readonly IServiceLocator serviceLocator;

		private ISession session;

		public DependencyInterceptor(IServiceLocator serviceLocator, IDictionary<string, Type> typesDictionary)
		{
			this.typesDictionary = typesDictionary;
			this.serviceLocator = serviceLocator;
		}

		public DependencyInterceptor(
			IServiceLocator dependencyContainer,
			IDictionary<string, Type> typesDictionary,
			Action<IComparable, object> setKey)
			: this(dependencyContainer, typesDictionary)
		{
			this.setKey = setKey;
		}

		public override void SetSession(ISession session)
		{
			this.session = session;
		}

		public override object Instantiate(string className, EntityMode entityMode, object id)
		{
			if (entityMode == EntityMode.Poco)
			{
				Type type = null;

				if (typesDictionary.ContainsKey(className))
				{
					type = typesDictionary[className];
				}

				if (type != null)
				{
					var instance = serviceLocator.GetService(type);

					if (instance == null)
					{
						return base.Instantiate(className, entityMode, id);
					}

					if (setKey != null)
					{
						setKey((IComparable)id, instance);
					}
					else
					{
						var md = session.SessionFactory.GetClassMetadata(className);
						md.SetIdentifier(instance, id, entityMode);
					}

					return instance;
				}
			}

			return base.Instantiate(className, entityMode, id);
		}
	}
}