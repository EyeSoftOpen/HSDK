namespace EyeSoft.Testing.Domain
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Extensions;

	public class MemoryTransactionalCollection : ReadOnlyTransactionalCollection, ITransactionalCollection
	{
		public MemoryTransactionalCollection()
		{
		}

		public MemoryTransactionalCollection(IEnumerable<IAggregate> data) : base(data)
		{
		}

		public void Save<T>(T entity) where T : class, IAggregate
		{
			if (!Collection.ContainsKey(entity.Id))
			{
				Add(entity);
				return;
			}

			Update(entity);
		}

		public void Delete<T>(T entity) where T : class, IAggregate
		{
			Collection.Remove(entity.Id);
		}

		public void Commit()
		{
		}

		private void Update<T>(T entity) where T : class, IAggregate
		{
			Collection[entity.Id] = entity;
		}

		private void Add<T>(T entity) where T : class, IAggregate
		{
			Collection.Add(entity.Id, entity);

			var children =
				entity
					.Extend().GetFlatternChildren(type => type.IsEnumerableOf<IAggregate>() || type.Implements<IAggregate>());

			children
				.OfType<IAggregate>()
				.ForEach(item =>
				{
					if (Collection.ContainsKey(item.Id))
					{
						return;
					}

					Collection.Add(item.Id, item);
				});
		}
	}
}