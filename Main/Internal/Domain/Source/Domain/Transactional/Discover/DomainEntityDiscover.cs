namespace EyeSoft.Domain.Transactional.Discover
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class DomainEntityDiscover
	{
		public static IEnumerable<Type> Entities<TUnitOfWork>()
			where TUnitOfWork : UnitOfWork
		{
			return Entities(typeof(TUnitOfWork));
		}

		public static IEnumerable<Type> Entities<TUnitOfWork>(IRepositoryDiscover repositoryDiscover) where TUnitOfWork : UnitOfWork
		{
			return Entities(typeof(TUnitOfWork), repositoryDiscover);
		}

		public static IEnumerable<Type> Entities(Type unitOfWorkType)
		{
			return Entities(unitOfWorkType, new RepositoryDiscover());
		}

		public static IEnumerable<Type> Entities(Type unitOfWorkType, IRepositoryDiscover repositoryDiscover)
		{
			var instanceProperties =
				unitOfWorkType
					.GetAnyInstanceProperties();

			var entitiesFromRepository =
				instanceProperties
					.Select(property => repositoryDiscover.GetEntityTypeIfRepository(property.PropertyType))
					.Where(entityType => entityType != null)
					.ToList();

			var relatedEntities =
				entitiesFromRepository
					.SelectMany(entityType => entityType.RelatedTypes(type => type.Implements<IEntity>()));

			return entitiesFromRepository.Union(relatedEntities).Distinct();
		}
	}
}