namespace EyeSoft.Domain.Transactional.Discover
{
	using System;
	using System.Linq;
	using System.Reflection;
    using EyeSoft.Extensions;
    using Repositories;

    public class RepositoryDiscover :
		IRepositoryDiscover
	{
		public Type GetEntityTypeIfRepository(Type type)
		{
			var entityType =
				GetEntityTypeByRepositoryIntercace(type);

			if (entityType.IsNotNull())
			{
				return entityType;
			}

			var constructors =
				type.GetConstructors(
					BindingFlags.Instance |
					BindingFlags.Public |
					BindingFlags.NonPublic);

			foreach (var constructor in constructors)
			{
				foreach (var parameter in constructor.GetParameters())
				{
					entityType = GetEntityTypeByRepositoryIntercace(parameter.ParameterType);

					if (entityType.IsNotNull())
					{
						return entityType;
					}
				}
			}

			return null;
		}

		private static Type GetEntityTypeByRepositoryIntercace(Type type)
		{
			var repositoryByInterface =
				type
					.GetInterfaces()
					.Where(interfaceType => interfaceType.IsGenericType)
					.SingleOrDefault(interfaceType => interfaceType.GetGenericTypeDefinition() == typeof(IReadOnlyRepository<>));

			if (repositoryByInterface.IsNull())
			{
				return null;
			}

			return repositoryByInterface.GetGenericArguments().Single();
		}
	}
}