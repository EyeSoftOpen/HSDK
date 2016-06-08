namespace EyeSoft.ActiveCampaign.Commanding.Contact
{
	using System;

	using EyeSoft.ActiveCampaign.Commanding.Contact.Models;
	using EyeSoft.ActiveCampaign.Query;

	public interface IContactCommandingClient : IDisposable
	{
		ActiveCampaignResponse Add(ContactAdd contact);

		ActiveCampaignResponse Delete(string contactId);
	}
}