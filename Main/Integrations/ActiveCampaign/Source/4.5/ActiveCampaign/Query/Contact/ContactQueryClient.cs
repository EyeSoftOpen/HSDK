namespace EyeSoft.ActiveCampaign.Query.Contact
{
	using System.Collections.Generic;
	using System.Linq;

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

		public IEnumerable<Contact> GetAll(int page = 0, string filterField = null, object[] filterValues = null)
		{
			var result = GetContacts("all", page, filterField, filterValues);

			if (result != null)
			{
				return result.Data;
			}

			return Enumerable.Empty<Contact>();
		}

		public Contact Get(string email)
		{
			return ExecuteGetRequest<Contact>("contact_view_email", new ContactViewEmailRequest(email));
		}

		private Contacts GetContacts(string ids, int page, string filterField = null, object[] filterValues = null)
		{
			if (!string.IsNullOrEmpty(filterField))
			{
				ids = null;
			}

			var request = new ContactsRequest { Ids = ids, Page = page, FilterField = filterField, FilterValues = filterValues };

			return ExecuteGetRequest<Contacts>("contact_list", request);
		}

		public IEnumerable<ContactAutomation> Automations(string email)
		{
			var request = new ContactAutomationListRequest(email);

			return ExecuteGetRequest<ContactAutomationList>("contact_automation_list", request)?.Data;
		}
	}
}