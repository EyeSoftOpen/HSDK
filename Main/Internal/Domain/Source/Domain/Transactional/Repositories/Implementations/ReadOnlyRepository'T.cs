namespace EyeSoft.Domain.Transactional.Repositories.Implementations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Aggregates;

    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class, IAggregate
	{
		private readonly IReadOnlyRepository<T> repository;

		protected internal ReadOnlyRepository(IReadOnlyRepository<T> repository)
		{
			this.repository = repository;
		}

		public Expression Expression => repository.Expression;

        public Type ElementType => repository.ElementType;

        public IQueryProvider Provider => repository.Provider;

        public virtual bool IsReadOnly => true;

        public IEnumerator<T> GetEnumerator()
		{
			return repository.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public T Load(IComparable key)
		{
			return repository.Load(key);
		}
	}
}