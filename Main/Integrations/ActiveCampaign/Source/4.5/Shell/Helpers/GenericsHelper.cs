namespace EyeSoft.ActiveCampaign.Shell.Helpers
{
	using System;

	using Newtonsoft.Json;

	public static class GenericsHelper
	{
		public static T Dump<T>(this T obj)
		{
			var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

			Console.WriteLine(json);

			return obj;
		}
	}
}
