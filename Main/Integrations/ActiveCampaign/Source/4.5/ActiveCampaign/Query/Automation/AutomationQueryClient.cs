namespace EyeSoft.ActiveCampaign.Query.Automation
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.ActiveCampaign.Query.Automation.Models;

	public class AutomationQueryClient : ActiveCampaignRestClient, IAutomationQueryClient
	{
		public AutomationQueryClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public IEnumerable<Automation> GetAll()
		{
			var request = new AutomationRequest();

			return ExecuteGetRequest<Automations>("automation_list", request).Data.OrderBy(x => x.Id);
		}
	}
}