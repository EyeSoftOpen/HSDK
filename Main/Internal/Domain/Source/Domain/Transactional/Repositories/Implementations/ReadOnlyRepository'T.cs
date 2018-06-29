namespace EyeSoft.Domain.Transactional.Implementations
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class, IAggregate
	{
		private readonly IReadOnlyRepository<T> repository;

		protected internal ReadOnlyRepository(IReadOnlyRepository<T> repository)
		{
			this.repository = repository;
		}

		public Expression Expression
		{
			get { return repository.Expression; }
		}

		public Type ElementType
		{
			get { return repository.ElementType; }
		}

		public IQueryProvider Provider
		{
			get { return repository.Provider; }
		}

		public virtual bool IsReadOnly
		{
			get { return true; }
		}

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