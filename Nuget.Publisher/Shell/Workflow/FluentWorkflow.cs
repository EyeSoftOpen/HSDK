namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public abstract class FluentWorkflow
	{
		private static int order;

		protected FluentWorkflow()
		{
			Console.WriteLine("{0}] {1}", ++order, GetType().Name);
		}
	}
}