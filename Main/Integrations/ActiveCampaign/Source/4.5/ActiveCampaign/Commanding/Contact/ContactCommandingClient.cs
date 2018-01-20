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

		public ActiveCampaignResponse Sync(SyncContactCommand command)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("contact_sync", command);
		}

		public ActiveCampaignResponse Delete(int id)
		{
			return ExecuteGetRequest<ActiveCampaignResponse>("contact_delete", new DeleteContactCommand(id));
		}

        public ActiveCampaignResponse AddTags(AddTagsCommand command)
        {
            return ExecutePostRequest<ActiveCampaignResponse>("contact_tag_add", command);
        }

        public ActiveCampaignResponse RemoveTags(RemoveTagsCommand command)
        {
            return ExecutePostRequest<ActiveCampaignResponse>("contact_tag_remove", command);
        }
    }
}