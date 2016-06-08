namespace EyeSoft.ActiveCampaign.Commanding.Automation
{
	using EyeSoft.ActiveCampaign.Commanding.Automation.Models;
	using EyeSoft.ActiveCampaign.Query;

	public class AutomationCommandingClient : ActiveCampaignRestClient, IAutomationCommandingClient
	{
		public AutomationCommandingClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public ActiveCampaignResponse AddContact(string contactEmail, string automation)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("automation_contact_add", new AutomationContactAdd(contactEmail, automation));
		}

		public ActiveCampaignResponse RemoveContact(string contactEmail, string automation)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("automation_contact_remove", new AutomationRemoveContact(contactEmail, automation));
		}
	}
}