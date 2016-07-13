namespace EyeSoft.ActiveCampaign.Query.Automation.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class AutomationContactListRequest : ActiveCampaignRequest
	{
		public AutomationContactListRequest(int automationId)
		{
			Automation = automationId;
		}

		public int Automation { get; }
	}
}