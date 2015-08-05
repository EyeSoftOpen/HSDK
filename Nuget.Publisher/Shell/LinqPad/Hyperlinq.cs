namespace EyeSoft.Nuget.Publisher.Shell.LinqPad
{
	using global::System;

	using Newtonsoft.Json;

	public class Hyperlinq
	{
		public Hyperlinq(Action action, string text)
		{
			Action = action;
			Text = text;
		}

		[JsonIgnore]
		public Action Action { get; private set; }

		public string Text { get; private set; }
	}
}