namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Serialization;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	public class Contacts
	{
		public IEnumerable<Contact> Data { get; set; }

		[JsonExtensionData(WriteData = false)]
#pragma warning disable 649
		private IDictionary<string, JToken> data;
#pragma warning restore 649

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			int result;

			var jsonList = data.Where(x => int.TryParse(x.Key, out result)).Select(x => x.Value.ToString()).ToArray();

			Data = jsonList.Select(JsonConvert.DeserializeObject<Contact>).ToArray();
		}
	}
}