namespace EyeSoft.ActiveCampaign.Commanding.Contact
{
	using System;

	using EyeSoft.ActiveCampaign.Commanding.Contact.Models;
	using EyeSoft.ActiveCampaign.Query;

	public interface IContactCommandingClient : IDisposable
	{
		ActiveCampaignResponse Add(AddContactCommand command);

		ActiveCampaignResponse Sync(SyncContactCommand command);

		ActiveCampaignResponse Delete(int id);

        ActiveCampaignResponse AddTags(AddTagsCommand command);

        ActiveCampaignResponse RemoveTags(RemoveTagsCommand command);
    }
}