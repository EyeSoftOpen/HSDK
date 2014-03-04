namespace EyeSoft.Data.EntityFramework
{
	using System;
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;

	using EyeSoft.Data.Common;
	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	public class EntityFrameworkReadonlyTransactionalCollection : IReadTransactionalCollection, IDatabaseSchema
	{
		protected readonly DbContext context;

		public EntityFrameworkReadonlyTransactionalCollection(string nameOrConnectionString, DbCompiledModel compiledModel)
		{
			context = new DbContextByModel(nameOrConnectionString, compiledModel);
		}

		protected EntityFrameworkReadonlyTransactionalCollection(DbConnection connection, DbCompiledModel compiledModel)
		{
			context = new DbContextByModel(connection, compiledModel);
		}

		protected EntityFrameworkReadonlyTransactionalCollection(DbContext dbContext)
		{
			context = dbContext;
		}

		public DbContext Context
		{
			get { return context; }
		}

		public IQueryable<T> Query<T>() where T : class, IAggregate
		{
			return context.Set<T>();
		}

		public T Load<T>(IComparable key) where T : class, IAggregate
		{
			return context.Set<T>().Find(key);
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public string Drop()
		{
			context.Database.Delete();
			return null;
		}

		public string Update()
		{
			throw new NotSupportedException();
		}

		public string Create()
		{
			context.Database.Create();
			return null;
		}

		public void Validate()
		{
			if (!context.Database.CompatibleWithModel(true))
			{
				throw new DatabaseSchemaNotValidException();
			}
		}
	}
}