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

		public ActiveCampaignResponse Add(AddContactCommand command)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("contact_add", command);
		}

		public ActiveCampaignResponse Sync(SyncContactCommand contact)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("contact_sync", contact);
		}

		public ActiveCampaignResponse Delete(int id)
		{
			return ExecuteGetRequest<ActiveCampaignResponse>("contact_delete", new DeleteContactCommand(id));
		}
	}
}