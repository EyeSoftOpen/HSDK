namespace EyeSoft.ActiveCampaign.Commanding.Automation
{
	using System;

	using EyeSoft.ActiveCampaign.Query;

	public interface IAutomationCommandingClient : IDisposable
	{
		ActiveCampaignResponse AddContact(string contactEmail, int automation);

		ActiveCampaignResponse RemoveContact(string contactEmail, int automation);
	}
}