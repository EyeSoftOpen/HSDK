namespace EyeSoft.Windows.Model.Execution
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