namespace EyeSoft.Data.Raven.Test.Helpers
{
	using global::Raven.Client;
	using global::Raven.Client.Document;
	using global::Raven.Client.Embedded;
	using global::Raven.Client.Extensions;

	public class RavenHelper
	{
		public const int Readings = 1000;

		private static IDocumentStore store;

		public RavenHelper(bool inMemory, string dbName = "default")
		{
			if (!inMemory)
			{
				store =
					new DocumentStore
						{
							Url = string.Concat("http://localhost:8080/databases/", dbName)
						}.Initialize();

				store.DatabaseCommands.EnsureDatabaseExists(dbName);

				return;
			}

			store =
				new EmbeddableDocumentStore
					{
						RunInMemory = true,
					}.Initialize();
		}

		public IDocumentSession CreateSession()
		{
			return store.OpenSession();
		}

		public void Dispose()
		{
			store.Dispose();
		}
	}
}