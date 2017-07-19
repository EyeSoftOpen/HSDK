namespace EyeSoft.ActiveCampaign.Commanding.Contact.Models
{
	using System.Collections.Generic;

	public class SyncContactCommand : ActiveCampaignRequest
	{
		public SyncContactCommand(string email)
		{
			Email = email;

			Fields = new Dictionary<string, string>();
		}

		public string Email { get; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Phone { get; set; }

		public string Orgname { get; set; }

		public string Tags { get; set; }

		public string PValues { get; set; }

		public IDictionary<string, string> Fields { get; }
	}
}