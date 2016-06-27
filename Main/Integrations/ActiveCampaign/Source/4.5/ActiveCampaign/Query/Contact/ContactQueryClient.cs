namespace EyeSoft.ActiveCampaign.Query.Contact
{
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.Contact.Models;

	public class ContactQueryClient : ActiveCampaignRestClient, IContactQueryClient
	{
		public ContactQueryClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public Contacts GetContacts(params int[] ids)
		{
			return GetContacts(string.Join(",", ids));
		}

		public IEnumerable<Contact> GetAll()
		{
			return GetContacts("all").Data;
		}

		public Contact Get(string email)
		{
			return ExecuteGetRequest<Contact>("contact_view_email", new ContactViewEmailRequest(email));
		}

		private Contacts GetContacts(string ids)
		{
			var request = new ContactsRequest { Ids = ids };

			return ExecuteGetRequest<Contacts>("contact_list", request);
		}
	}
}