namespace EyeSoft.Nuget.Publisher.Core.LinqPad
{
	using System;

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

		public override string ToString()
		{
			return Text;
		}
	}
}