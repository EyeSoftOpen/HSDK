namespace EyeSoft.Testing.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	public class ReadOnlyTransactionalCollection
		: IReadTransactionalCollection
	{
		private readonly IDictionary<IComparable, IAggregate> collection =
			new Dictionary<IComparable, IAggregate>();

		public ReadOnlyTransactionalCollection()
		{
			Collection = collection;
		}

		public ReadOnlyTransactionalCollection(IEnumerable<IAggregate> data)
			: this()
		{
			data
				.ForEach(item => collection.Add(item.Id, item));
		}

		protected IDictionary<IComparable, IAggregate> Collection { get; private set; }

		public IQueryable<T> Query<T>() where T : class, IAggregate
		{
			return Collection.Values.OfType<T>().AsQueryable();
		}

		public T Load<T>(IComparable key) where T : class, IAggregate
		{
			if (!Collection.ContainsKey(key))
			{
				return null;
			}

			return (T)Collection[key];
		}

		public void Dispose()
		{
		}
	}
}