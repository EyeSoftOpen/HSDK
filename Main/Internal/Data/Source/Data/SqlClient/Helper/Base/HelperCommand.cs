namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	public abstract class HelperCommand : IHelperCommand
	{
		protected readonly SqlCommand command;

		protected HelperCommand(SqlCommand command)
		{
			this.command = command;
		}

		public virtual void Execute()
		{
			using (command)
			{
				command.ExecuteNonQuery();
			}
		}
	}
}