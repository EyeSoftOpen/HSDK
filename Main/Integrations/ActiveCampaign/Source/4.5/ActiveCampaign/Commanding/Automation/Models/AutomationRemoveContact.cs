namespace EyeSoft.ActiveCampaign.Commanding.Automation.Models
{
	public class AutomationRemoveContact : ActiveCampaignRequest
	{
		public AutomationRemoveContact(string contactEmail, string automation)
		{
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string ContactEmail { get; }

		public string Automation { get; }
	}
}