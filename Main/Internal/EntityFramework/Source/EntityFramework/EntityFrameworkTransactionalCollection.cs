namespace EyeSoft.Data.EntityFramework
{
	using System;
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;

	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	public class EntityFrameworkTransactionalCollection : EntityFrameworkReadonlyTransactionalCollection, ITransactionalCollection
	{
		public EntityFrameworkTransactionalCollection(string nameOrConnectionString, DbCompiledModel model)
			: base(nameOrConnectionString, model)
		{
		}

		public EntityFrameworkTransactionalCollection(DbConnection connection, DbCompiledModel model)
			: base(connection, model)
		{
		}

		public EntityFrameworkTransactionalCollection(Func<DbConnection> createConnection, DbCompiledModel model)
			: base(createConnection(), model)
		{
		}

		public EntityFrameworkTransactionalCollection(DbContext dbContext) : base(dbContext)
		{
		}

		public void Save<T>(T entity) where T : class, IAggregate
		{
			var set = context.Set<T>();

			var loaded = set.Find(entity.Id);

			var isNew = loaded == null;

			if (isNew)
			{
				context.Entry(entity).State = EntityState.Added;
			}
			else
			{
				context.Entry(loaded).CurrentValues.SetValues(entity);
			}
		}

		public void Delete<T>(T entity) where T : class, IAggregate
		{
			var entry = context.Entry(entity);
			entry.State = EntityState.Deleted;
		}

		public void Commit()
		{
			context.SaveChanges();
		}
	}
}