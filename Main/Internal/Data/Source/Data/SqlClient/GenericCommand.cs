namespace EyeSoft.Data.SqlClient
{
	using System.Data.SqlClient;

	using EyeSoft.Data.SqlClient.Helper;
    using Helper.Base;

    public class GenericCommand : HelperCommand
	{
		public GenericCommand(SqlCommand command, string commandText)
			: base(command)
		{
			command.CommandText = commandText;
		}

		public override void Execute()
		{
			command.ExecuteNonQuery();
		}
	}
}