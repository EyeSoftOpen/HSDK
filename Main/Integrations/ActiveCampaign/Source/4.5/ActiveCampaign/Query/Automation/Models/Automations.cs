namespace EyeSoft.ActiveCampaign.Query.Automation.Models
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	using EyeSoft.ActiveCampaign.Helpers;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	internal class Automations
	{
		public IEnumerable<Automation> Data { get; private set; }

		[JsonExtensionData(WriteData = false)]
		private IDictionary<string, JToken> data;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			int result;

			Data =
				data
					.Where(x => int.TryParse(x.Key, out result))
					.Select(x => x.Value.ToString())
					.Select(JsonConvertWrapper.DeserializeObject<Automation>);
		}
	}
}