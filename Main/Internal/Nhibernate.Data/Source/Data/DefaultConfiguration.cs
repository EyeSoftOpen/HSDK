namespace EyeSoft.Data.Nhibernate
{
	using EyeSoft.Data.Common;

	using NHibernate.Cfg;
	using NHibernate.Cfg.Loquacious;
	using NHibernate.Context;

	public class DefaultConfiguration
		: IConfigurationApplier
	{
		private readonly SchemaAction schemaAction;

		public DefaultConfiguration(SchemaAction schemaAction)
		{
			this.schemaAction = schemaAction;
		}

		public SchemaAction SchemaAction => schemaAction;

        public virtual void Apply(Configuration configuration, IDatabaseProvider databaseProvider)
		{
			configuration.CurrentSessionContext<CallSessionContext>();

			configuration.DataBaseIntegration(db => SetDatabase(db, databaseProvider, configuration));

			configuration.Cache(SetCache);

			configuration.Proxy(SetProxy);
		}

		protected virtual void SetDatabase(
			IDbIntegrationConfigurationProperties db,
			IDatabaseProvider databaseProvider,
			Configuration configuration)
		{
			db.Dialect(databaseProvider.ProviderName);
			db.ConnectionString = databaseProvider.ConnectionString;
			db.AutoCommentSql = true;
			db.IsolationLevel = System.Data.IsolationLevel.ReadCommitted;
			db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
			db.PrepareCommands = true;
		}

		protected virtual void SetProxy(IProxyConfigurationProperties proxy)
		{
			proxy.Validation = true;
		}

		protected virtual void SetCache(ICacheConfigurationProperties cache)
		{
		}
	}
}