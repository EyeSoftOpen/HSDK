namespace EyeSoft.Windows.Model
{
	using System.Windows;

	public class AsyncExecution : Execution
	{
		internal AsyncExecution(Application application)
			: base(application, true)
		{
		}
	}
}