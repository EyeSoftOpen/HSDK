using System.Windows;

namespace EyeSoft.Windows.Model.Execution
{
	public class AsyncExecution : Execution
	{
		internal AsyncExecution(Application application)
			: base(application, true)
		{
		}
	}
}