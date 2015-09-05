namespace Domain
{
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using System.Data.SqlClient;

	public class FinancialDbContext : DbContext
	{
		public FinancialDbContext(SqlConnectionStringBuilder connectionStringBuilder)
			: this(connectionStringBuilder.ConnectionString)
		{
		}

		public FinancialDbContext(string connectionString) : this(DbProviderFactories.GetFactory("System.Data.SqlClient"), connectionString)
		{
		}

		public FinancialDbContext(DbProviderFactory factory, string connectionString) : base(CreateConnection(factory, connectionString), true)
		{
			Database.SetInitializer(new CreateDatabaseIfNotExists<FinancialDbContext>());
			Database.Initialize(true);
		}

		public IDbSet<Estimate> EstimateRepository => Set<Estimate>();

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		private static DbConnection CreateConnection(DbProviderFactory factory, string connectionString)
		{
			var connection = factory.CreateConnection();
			connection.ConnectionString = connectionString;
			return connection;
		}
	}
}