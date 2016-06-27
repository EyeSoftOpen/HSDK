namespace EyeSoft.ActiveCampaign.Commanding.Contact.Models
{
	using System.Collections.Generic;

	public class AddContactCommand : ActiveCampaignRequest
	{
		public AddContactCommand(int id, string email)
		{
			Id = id;
			Email = email;

			var fields = new Dictionary<string, string> { { "345", id.ToString() } };

			Fields = fields;
		}

		public int Id { get; }

		public string Email { get; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Phone { get; set; }

		public string Orgname { get; set; }

		public string Tags { get; set; }

		public IReadOnlyDictionary<string, string> Fields { get; }
	}
}
