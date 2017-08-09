namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
    using System.Collections.Generic;
    using System.Linq;
	using System.Runtime.Serialization;

	using EyeSoft.ActiveCampaign.Helpers;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

    internal class Contacts
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

			Data = jsonList.Select(JsonConvertWrapper.DeserializeObject<Contact>).ToArray();
		}
	}
}