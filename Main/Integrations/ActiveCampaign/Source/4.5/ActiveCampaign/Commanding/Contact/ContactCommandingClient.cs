namespace EyeSoft.ActiveCampaign.Commanding.Contact
{
	using EyeSoft.ActiveCampaign.Commanding.Contact.Models;
	using EyeSoft.ActiveCampaign.Query;

	public class ContactCommandingClient : ActiveCampaignRestClient, IContactCommandingClient
	{
		public ContactCommandingClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public ActiveCampaignResponse Add(ContactAdd contact)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("contact_add", contact);
		}

		public ActiveCampaignResponse Delete(string contactId)
		{
			return ExecuteGetRequest<ActiveCampaignResponse>("contact_delete", new ContactDelete(contactId));
		}
	}
}