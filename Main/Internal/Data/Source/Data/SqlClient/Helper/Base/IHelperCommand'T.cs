namespace EyeSoft.Data.SqlClient.Helper
{
	public interface IHelperCommand<out TResult>
	{
		TResult Execute();
	}
}