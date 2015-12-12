namespace EyeSoft.Nuget.Publisher.Shell.Workflow
{
	using System;

	public class WaitStep : FluentWorkflowStep
	{
		public void Wait()
		{
			Console.ReadLine();
		}
	}
}