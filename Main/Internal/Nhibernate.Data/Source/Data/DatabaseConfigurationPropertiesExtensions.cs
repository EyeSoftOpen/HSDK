namespace EyeSoft.Data.Nhibernate
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Data.Common;

	using NHibernate.Cfg.Loquacious;
	using NHibernate.Dialect;

	public static class DatabaseConfigurationPropertiesExtensions
	{
		private static readonly IDictionary<string, Type> dialectDictionary =
			new Dictionary<string, Type>
				{
					{ DatabaseProviders.SqlServer, typeof(MsSql2008Dialect) },
					{ DatabaseProviders.SqLite, typeof(SQLiteDialect) },
					{ DatabaseProviders.SqlCe40, typeof(MsSqlCe40Dialect) },
					{ DatabaseProviders.MySql, typeof(MySQLDialect) },
					{ DatabaseProviders.MySql5, typeof(MySQL5Dialect) }
				};

		public static void Dialect(this IDbIntegrationConfigurationProperties configuration, string providerName)
		{
			configuration
				.GetType()
				.GetMethod("Dialect")
				.MakeGenericMethod(dialectDictionary[providerName])
				.Invoke(configuration, null);
		}
	}
}