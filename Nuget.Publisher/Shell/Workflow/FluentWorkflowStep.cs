namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public abstract class FluentWorkflowStep
	{
		private static int order;

		protected FluentWorkflowStep()
		{
			Console.WriteLine("{0}] {1}", ++order, GetType().Name);
		}
	}
}