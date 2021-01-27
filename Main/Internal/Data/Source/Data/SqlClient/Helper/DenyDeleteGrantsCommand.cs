namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;
    using Base;

    public class DenyDeleteGrantsCommand : HelperCommand
	{
		private const string CommandText = "DENY DELETE ON @tableName TO @userOrRole";

		private readonly string userOrRole;

		private readonly string tableName;

		public DenyDeleteGrantsCommand(SqlCommand connection, string userOrRole, string tableName)
			: base(connection)
		{
			this.userOrRole = userOrRole;
			this.tableName = tableName;
		}

		public override void Execute()
		{
			using (command)
			{
				command.CommandText = CommandText;
				command.Parameters.AddWithValue("@tableName", tableName);
				command.Parameters.AddWithValue("@userOrRole", userOrRole);

				command.ExecuteNonQuery();
			}
		}
	}
}