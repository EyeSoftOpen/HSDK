namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	[DebuggerDisplay("{Email}")]
	public class Contact
	{
		public string Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime CDate { get; set; }

		public string ListName { get; set; }

		public string Ua { get; set; }

		public string Email { get; set; }

		public IEnumerable<ContactAction> Actions { get; set; }
	}
}