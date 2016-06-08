namespace EyeSoft.ActiveCampaign.Commanding.Automation.Models
{
	public class AutomationContactAdd : ActiveCampaignRequest
	{
		public AutomationContactAdd(string contactEmail, string automation)
		{
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string ContactEmail { get; }

		public string Automation { get; }
	}
}
