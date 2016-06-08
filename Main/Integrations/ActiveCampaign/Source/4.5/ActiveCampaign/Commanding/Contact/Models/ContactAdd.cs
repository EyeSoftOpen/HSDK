namespace EyeSoft.ActiveCampaign.Commanding.Contact.Models
{
	using System.Collections.Generic;

	public class ContactAdd : ActiveCampaignRequest
	{
		public ContactAdd(string id, string email)
		{
			Email = email;

			var fields = new Dictionary<string, string> { { "345", id } };

			Fields = fields;
		}

		public string Email { get; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Phone { get; set; }

		public string Orgname { get; set; }

		public string Tags { get; set; }

		public IReadOnlyDictionary<string, string> Fields { get; }
	}
}
