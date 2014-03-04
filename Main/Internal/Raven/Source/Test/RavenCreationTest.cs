namespace EyeSoft.Data.Raven.Test
{
	using EyeSoft.Data.Raven.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using global::Raven.Client.Document;

	using global::Raven.Client.Extensions;

	[TestClass]
	public class RavenCreationTest
	{
		[TestMethod]
		public void CreateDbAndLoad()
		{
			const string DbName = "CreationTest";

			using (var documentStore = new DocumentStore { Url = "http://localhost:8080/databases/" + DbName }.Initialize())
			{
				documentStore.DatabaseCommands.EnsureDatabaseExists(DbName);

				using (var session = documentStore.OpenSession())
				{
					session.Load<Order>("1");
				}
			}
		}
	}
}