namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Collections.Generic;
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;

	public class DatabaseListCommand : HelperCommand<string[]>
	{
		private const string SelectDatabaseNameCommandText =
			@"select [name] from sys.databases where database_id > 0";

		public DatabaseListCommand(SqlCommand command)
			: base(command)
		{
		}

		public override string[] Execute()
		{
			command.CommandText = SelectDatabaseNameCommandText;

			using (var reader = command.ExecuteReader())
			{
				var databaseList = new List<string>();

				while (reader.Read())
				{
					var name = reader.Column<string>("name");

					databaseList.Add(name);
				}

				return databaseList.ToArray();
			}
		}
	}
}