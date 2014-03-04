namespace EyeSoft.Domain.Transactional.Discover
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;

	public static class UnitOfWorkEntities
	{
		public static IEnumerable<MappedType> Discover<TUnitOfWork>()
			where TUnitOfWork : UnitOfWork
		{
			return
				DomainEntityDiscover
					.Entities(typeof(TUnitOfWork))
					.Select(type => TypeMapperFactory.CreateByConventions().Map(type));
		}
	}
}