namespace EyeSoft.Transfer.Service.Test.Chunking
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	internal class MemoryTransactionalCollectionFactory : ITransactionalCollectionFactory
	{
		public ITransactionalCollection Create()
		{
			return new MemoryTransactionalCollection();
		}
	}

	internal class MemoryTransactionalCollection : ITransactionalCollection
	{
		private readonly IDictionary<object> list = new Dictionary<IComparable, object>();

		private readonly IDictionary<IComparable, object> list = new Dictionary<IComparable, object>();

		public IQueryable<T> Query<T>() where T : class, IAggregate
		{
			return list.Values.OfType<T>().AsQueryable();
		}

		public T Load<T>(IComparable key) where T : class, IAggregate
		{
			return (T)list[key];
		}

		public void Save<T>(T entity) where T : class, IAggregate
		{
			list[entity.Id] = entity;
		}

		public void Delete<T>(T entity) where T : class, IAggregate
		{
			list.Remove(entity.Id);
		}

		public void Commit()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}
	}
}