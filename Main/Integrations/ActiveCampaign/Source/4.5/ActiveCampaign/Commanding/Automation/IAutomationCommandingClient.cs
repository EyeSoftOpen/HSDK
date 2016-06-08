namespace EyeSoft.ActiveCampaign.Commanding.Automation
{
	using System;

	using EyeSoft.ActiveCampaign.Query;

	public interface IAutomationCommandingClient : IDisposable
	{
		ActiveCampaignResponse AddContact(string contactEmail, string automation);

		ActiveCampaignResponse RemoveContact(string contactEmail, string automation);
	}
}