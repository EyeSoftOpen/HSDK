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

		public Contacts GetContacts(int page = 0, params int[] ids)
		{
			return GetContacts(string.Join(",", ids), page);
		}

		public IEnumerable<Contact> GetAll(int page = 0)
		{
			return GetContacts("all", page).Data;
		}

		public Contact Get(string email)
		{
			return ExecuteGetRequest<Contact>("contact_view_email", new ContactViewEmailRequest(email));
		}

		private Contacts GetContacts(string ids, int page)
		{
			var request = new ContactsRequest { Ids = ids, Page = page };

			return ExecuteGetRequest<Contacts>("contact_list", request);
		}

		public IEnumerable<ContactAutomation> Automations(string email)
		{
			var request = new ContactAutomationListRequest(email);

			return ExecuteGetRequest<ContactAutomationList>("contact_automation_list", request)?.Data;
		}
	}
}