namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class ContactsRequest : ActiveCampaignRequest
	{
		public string Ids { get; set; }

		public int Page { get; set; }

		public object[] FilterValues { get; set; }

		public string FilterField { get; set; }
	}
}