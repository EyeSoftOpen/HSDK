namespace EyeSoft.Data.SqlClient.Helper.Base
{
	public interface IHelperCommand<out TResult>
	{
		TResult Execute();
	}
}