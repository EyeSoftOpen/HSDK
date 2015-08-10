namespace EyeSoft.Nuget.Publisher.Shell
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