namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class ContactsRequest : ActiveCampaignRequest
	{
		public string Ids { get; set; }

		//		public string Filters { get; set; }

		//		public string Sort Field to sort on(possible values: id, datetime, first_name, last_name).
		//sort_direction Direction of sort(possible values: ASC or DESC).
		//page
	}
}