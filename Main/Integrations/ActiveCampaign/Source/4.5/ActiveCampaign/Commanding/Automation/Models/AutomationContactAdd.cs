namespace EyeSoft.ActiveCampaign.Commanding.Automation.Models
{
	public class AutomationContactAdd : ActiveCampaignRequest
	{
		public AutomationContactAdd(string contactEmail, int automation)
		{
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string ContactEmail { get; }

		public int Automation { get; }
	}
}
