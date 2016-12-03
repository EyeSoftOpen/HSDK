namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class ContactsRequest : ActiveCampaignRequest
	{
		public string Ids { get; set; }

	    public int Page { get; set; }
	}
}