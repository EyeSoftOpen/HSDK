namespace EyeSoft.Domain.Transactional.UnitOfWork
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Aggregates;
    using Repositories;
    using Repositories.Implementations;

    public abstract class ReadOnlyUnitOfWork : IDisposable
	{
		private readonly IReadTransactionalCollection transactionalCollection;

		protected ReadOnlyUnitOfWork(IReadTransactionalCollection transactionalCollection)
		{
			this.transactionalCollection = transactionalCollection;

			AutoInitializeRepository();
		}

		public virtual void Dispose()
		{
			transactionalCollection.Dispose();
		}

		protected virtual void AutoInitializeRepository()
		{
			AutoInitializeRepository(typeof(IReadOnlyRepository<>), "ReadRepository");
		}

		protected void AutoInitializeRepository(Type repositoryInterfaceType, string repositoryMethodName)
		{
			var properties = GetType().GetProperties();

			foreach (var property in properties)
			{
				if (TypeIsRepository(repositoryInterfaceType, property.PropertyType))
				{
					SetDefautlRepositoryOnProperty(repositoryMethodName, property.PropertyType, property, null);

					continue;
				}

				SetCustomRepositoryOnProperty(repositoryInterfaceType, repositoryMethodName, property);
			}
		}

		protected IReadOnlyRepository<T> ReadRepository<T>() where T : class, IAggregate
		{
			return new ReadOnlyRepository<T>(new InternalReadOnlyRepository<T>(transactionalCollection));
		}

		private void SetCustomRepositoryOnProperty(Type repositoryInterfaceType, string repositoryMethodName, PropertyInfo property)
		{
			var constructors = property.PropertyType.GetConstructors();

			foreach (var constructor in constructors)
			{
				var parameters = constructor.GetParameters();

				if (parameters.Count() != 1)
				{
					continue;
				}

				var parameterType = parameters.Single().ParameterType;

				if (!TypeIsRepository(repositoryInterfaceType, parameterType))
				{
					continue;
				}

				SetDefautlRepositoryOnProperty(repositoryMethodName, parameterType, property, constructor);
			}
		}

		private void SetDefautlRepositoryOnProperty(
			string repositoryMethodName,
			Type repositoryType,
			PropertyInfo property,
			ConstructorInfo strongTypedRepositoryConstructor)
		{
			var parameterArgument = repositoryType.GetGenericArguments().Single();

			var repositoryMethod =
				GetType()
				.GetMethod(repositoryMethodName, BindingFlags.Instance | BindingFlags.NonPublic)
				.MakeGenericMethod(parameterArgument);

			var repository = repositoryMethod.Invoke(this, null);

			if (strongTypedRepositoryConstructor == null)
			{
				property.SetValue(this, repository, null);
				return;
			}

			property.SetValue(this, strongTypedRepositoryConstructor.Invoke(new[] { repository }), null);
		}

		private bool TypeIsRepository(Type repositoryInterfaceType, Type repositoryType)
		{
			var typeIsRepository =
				repositoryType.IsGenericType &&
				repositoryType.GetGenericTypeDefinition() == repositoryInterfaceType;

			return typeIsRepository;
		}

		protected class InternalReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class, IAggregate
		{
			private readonly IReadTransactionalCollection transactionalCollection;

			private readonly IQueryable<T> queryable;

			public InternalReadOnlyRepository(IReadTransactionalCollection transactionalCollection)
			{
				this.transactionalCollection = transactionalCollection;
				queryable = transactionalCollection.Query<T>();
			}

			public Expression Expression => queryable.Expression;

            public Type ElementType => queryable.ElementType;

            public IQueryProvider Provider => queryable.Provider;

            public T Load(IComparable key)
			{
				return transactionalCollection.Load<T>(key);
			}

			public IQueryable<T> Include<TRelated>(Func<T, TRelated> path)
			{
				return transactionalCollection.Query<T>();
			}

			public IEnumerator<T> GetEnumerator()
			{
				return queryable.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
	}
}