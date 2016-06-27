namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using Newtonsoft.Json;

	[DebuggerDisplay("{Email}")]
	public class Contact
	{
		public int Id { get; set; }

		public string Email { get; set; }

		[JsonProperty(PropertyName = "first_name")]
		public string FirstName { get; set; }

		[JsonProperty(PropertyName = "last_name")]
		public string LastName { get; set; }

		[JsonProperty(PropertyName = "sdate")]
		public DateTime? Subscribed { get; set; }

		[JsonProperty(PropertyName = "udate")]
		public DateTime? Unsubscribed { get; set; }

		[JsonProperty(PropertyName = "cdate")]
		public DateTime Creation { get; set; }

		public string ListName { get; set; }

		public string Ua { get; set; }

		public IEnumerable<ContactAction> Actions { get; set; }
	}
}