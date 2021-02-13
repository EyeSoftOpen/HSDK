namespace EyeSoft.Data.Nhibernate
{
	using System;
	using System.Collections.Generic;
    using Extensions;
    using EyeSoft.Data.Common;
    using NHibernate;
	using NHibernate.Cfg;
	using NHibernate.Cfg.MappingSchema;
	using NHibernate.Stat;

	public class NhibernateHelper
		: IDatabaseSchema
	{
		private static readonly IDictionary<SchemaAction, Action<IDatabaseSchema>> schemaActionDictionary =
			new Dictionary<SchemaAction, Action<IDatabaseSchema>>
				{
					{ SchemaAction.None, databaseSchema => { } },
					{ SchemaAction.Create, databaseSchema => databaseSchema.Create() },
					{ SchemaAction.Update, databaseSchema => databaseSchema.Update() },
					{ SchemaAction.Validate, databaseSchema => databaseSchema.Validate() },
					{ SchemaAction.Recreate, databaseSchema => databaseSchema.Create() }
				};

		private readonly IInterceptor interceptor;

		private readonly Configuration configuration =
			new Configuration();

		private readonly NhibernateDatabaseSchema databaseSchema;

		private ISessionFactory sessionFactory;

		public NhibernateHelper(IDatabaseProvider databaseProvider, HbmMapping mapping)
			: this(databaseProvider, mapping, new DefaultConfiguration(SchemaAction.None))
		{
		}

		public NhibernateHelper(
			IInterceptor interceptor,
			IDatabaseProvider databaseProvider,
			HbmMapping mapping,
			IConfigurationApplier configurationApplier)
				: this(databaseProvider, mapping, configurationApplier)
		{
			this.interceptor = interceptor;
		}

		public NhibernateHelper(IInterceptor interceptor, IDatabaseProvider databaseProvider, HbmMapping mapping)
				: this(interceptor, databaseProvider, mapping, new DefaultConfiguration(SchemaAction.None))
		{
		}

		public NhibernateHelper(IDatabaseProvider databaseProvider, HbmMapping mapping, IConfigurationApplier configurationApplier)
		{
			configurationApplier.Apply(Configuration, databaseProvider);

			Configuration.AddDeserializedMapping(mapping, null);

			databaseSchema = new NhibernateDatabaseSchema(databaseProvider, configuration);

			ExecuteSchemaAction(configurationApplier.SchemaAction, databaseSchema);
		}

		public Configuration Configuration => configuration;

        public IStatistics Statistics => CreateSessionFactory().Statistics;

        public ISession OpenSession()
		{
			return
				interceptor == null ?
					CreateSessionFactory().OpenSession() :
					CreateSessionFactory().OpenSession(interceptor);
		}

		public string Drop()
		{
			return databaseSchema.Drop();
		}

		public string Update()
		{
			return databaseSchema.Update();
		}

		public string Create()
		{
			return databaseSchema.Create();
		}

		public void Validate()
		{
			databaseSchema.Validate();
		}

		public void Dispose()
		{
			if (!sessionFactory.IsNull())
			{
				sessionFactory.Dispose();
			}
		}

		private ISessionFactory CreateSessionFactory()
		{
			if (sessionFactory.IsNull())
			{
				sessionFactory = Configuration.BuildSessionFactory();
			}

			return sessionFactory;
		}

		private void ExecuteSchemaAction(
			SchemaAction schemaAction,
			IDatabaseSchema databaseSchema)
		{
			schemaActionDictionary[schemaAction](databaseSchema);
		}
	}
}