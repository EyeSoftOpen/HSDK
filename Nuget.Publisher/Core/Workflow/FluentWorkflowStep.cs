namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using System;

	using EyeSoft.Nuget.Publisher.Core.Core;

	public abstract class FluentWorkflowStep
	{
		private static int order;

		protected FluentWorkflowStep()
		{
			var step = GetType().Name.Replace("Step", null).SplitWordsFirstWordCapital();

			Console.WriteLine("{0}] {1}", ++order, step);
		}
	}
}