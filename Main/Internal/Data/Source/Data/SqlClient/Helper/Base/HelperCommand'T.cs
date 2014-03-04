namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	public abstract class HelperCommand<TResult> : IHelperCommand<TResult>
	{
		protected readonly SqlCommand command;

		protected HelperCommand(SqlCommand command)
		{
			this.command = command;
		}

		public abstract TResult Execute();
	}
}