namespace EyeSoft.Domain.Transactional.UnitOfWork
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Aggregates;
    using Repositories;
    using Repositories.Implementations;

    public abstract class UnitOfWork : ReadOnlyUnitOfWork
	{
		private readonly ITransactionalCollection transactionalCollection;

		protected UnitOfWork(ITransactionalCollection transactionalCollection) : base(transactionalCollection)
		{
			this.transactionalCollection = transactionalCollection;

			AutoInitializeRepository();
		}

		public virtual void Commit()
		{
			transactionalCollection.Commit();
		}

		protected override void AutoInitializeRepository()
		{
			base.AutoInitializeRepository();
			AutoInitializeRepository(typeof(IRepository<>), "Repository");
		}

		protected IRepository<T> Repository<T>() where T : class, IAggregate
		{
			return new Repository<T>(new InternalRepository<T>(ReadRepository<T>(), transactionalCollection, Commit));
		}

		private class InternalRepository<T> : IRepository<T> where T : class, IAggregate
		{
			private readonly IReadOnlyRepository<T> readOnlyRepository;

			private readonly ITransactionalCollection transactionalCollection;

			private readonly Action commit;

			public InternalRepository(
				IReadOnlyRepository<T> readOnlyRepository,
				ITransactionalCollection transactionalCollection,
				Action commit)
			{
				this.readOnlyRepository = readOnlyRepository;
				this.transactionalCollection = transactionalCollection;
				this.commit = commit;
			}

			public Expression Expression => readOnlyRepository.Expression;

            public Type ElementType => readOnlyRepository.ElementType;

            public IQueryProvider Provider => readOnlyRepository.Provider;

            public T Load(IComparable key)
			{
				return readOnlyRepository.Load(key);
			}

			public IEnumerator<T> GetEnumerator()
			{
				return readOnlyRepository.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Save(T entity)
			{
				transactionalCollection.Save(entity);
			}

			public bool Remove(T entity)
			{
				transactionalCollection.Delete(entity);
				return true;
			}

			public void Commit()
			{
				commit();
			}
		}
	}
}