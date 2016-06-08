namespace EyeSoft.Nuget.Publisher.Core.LinqPad
{
	using System;

	using Newtonsoft.Json;

	public static class ObjectExtensions
	{
		public static T Dump<T>(this T instance, string title = null)
		{
			var json = JsonConvert.SerializeObject(instance, Formatting.Indented);

			if (title != null)
			{
				Console.WriteLine(title + ": ");
			}

			Console.WriteLine(json);

			return instance;
		}
	}
}