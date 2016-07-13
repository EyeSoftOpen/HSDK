namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	internal class ContactAutomationListRequest : ActiveCampaignRequest
	{
		public ContactAutomationListRequest(string email)
		{
			ContactEmail = email;
		}

		public string ContactEmail { get; }
	}
}