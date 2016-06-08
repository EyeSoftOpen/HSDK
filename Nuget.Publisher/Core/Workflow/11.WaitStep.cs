namespace EyeSoft.Nuget.Publisher.Core.Workflow
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