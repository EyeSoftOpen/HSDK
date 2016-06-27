namespace EyeSoft.ActiveCampaign.Commanding.Automation.Models
{
	public class AutomationRemoveContact : ActiveCampaignRequest
	{
		public AutomationRemoveContact(string contactEmail, int automation)
		{
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string ContactEmail { get; }

		public int Automation { get; }
	}
}