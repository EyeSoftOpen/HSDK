namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using Newtonsoft.Json;

	[DebuggerDisplay("{Email}")]
	public class Contact
	{
	    private const string NullDateString = "0000-00-00 00:00:00";

	    public int Id { get; set; }

		public string Email { get; set; }

		[JsonProperty(PropertyName = "first_name")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "last_name")]
		public string LastName { get; set; }

        [JsonProperty(PropertyName = "sdate")]
        public string SubscribedDate { get; set; }

		public DateTime? Subscribed => ConvertDateTime(SubscribedDate);

        [JsonProperty(PropertyName = "udate")]
		public string UnsubscribedDate { get; set; }

        public DateTime? Unsubscribed => ConvertDateTime(UnsubscribedDate);

        [JsonProperty(PropertyName = "cdate")]
		public DateTime Creation { get; set; }

		public string ListName { get; set; }

		public string Ua { get; set; }

		public IEnumerable<ContactAction> Actions { get; set; }

		private DateTime? ConvertDateTime(string dateTimeString)
		{
		    if (string.IsNullOrEmpty(dateTimeString) || dateTimeString == NullDateString)
			{
				return null;
			}

			return DateTime.Parse(dateTimeString);
		}
	}
}