namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using System;
	using System.Diagnostics;

	[DebuggerDisplay("{Text}")]
	public class ContactAction
	{
		public DateTime Tstamp { get; set; }

		public string Text { get; set; }

		public string Type { get; set; }
	}
}