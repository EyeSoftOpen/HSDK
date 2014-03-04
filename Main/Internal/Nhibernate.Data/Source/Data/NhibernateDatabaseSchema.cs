namespace EyeSoft.Data.Nhibernate
{
	using System.Text;

	using EyeSoft.Data.Common;

	using NHibernate.Cfg;
	using NHibernate.Tool.hbm2ddl;

	public class NhibernateDatabaseSchema
		: IDatabaseSchema
	{
		private readonly IDatabaseProvider databaseProvider;
		private readonly Configuration configuration;

		public NhibernateDatabaseSchema(IDatabaseProvider databaseProvider, Configuration configuration)
		{
			this.databaseProvider = databaseProvider;
			this.configuration = configuration;
		}

		public string Drop()
		{
			var schemaExport = new SchemaExport(configuration);

			var stringBuilder = new StringBuilder();
			schemaExport.Execute(x => stringBuilder.Append(x), true, true);

			var script = stringBuilder.ToString();

			return
				string.IsNullOrWhiteSpace(script) ? null : script;
		}

		public string Update()
		{
			var schemaExport = new SchemaUpdate(configuration);

			var stringBuilder = new StringBuilder();
			schemaExport.Execute(x => stringBuilder.Append(x), false);

			var script = stringBuilder.ToString();

			return string.IsNullOrWhiteSpace(script) ? null : script;
		}

		public string Create()
		{
			var schemaExport = new SchemaExport(configuration);

			databaseProvider.CreateIfNotExists();

			var stringBuilder = new StringBuilder();
			schemaExport.Create(x => stringBuilder.Append(x), false);
			var statement = stringBuilder.ToString();
			statement = string.IsNullOrWhiteSpace(statement) ? null : statement;

			if (!databaseProvider.Exists())
			{
				databaseProvider.Create();
				schemaExport.Execute(false, true, false);
			}
			else
			{
				try
				{
					new SchemaValidator(configuration).Validate();
				}
				catch
				{
					schemaExport.Execute(false, true, false);
				}
			}

			return statement;
		}

		public void Validate()
		{
			new SchemaValidator(configuration).Validate();
		}

		public void Dispose()
		{
		}
	}
}