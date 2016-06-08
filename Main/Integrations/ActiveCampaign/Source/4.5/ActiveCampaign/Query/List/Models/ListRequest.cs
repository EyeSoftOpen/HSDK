namespace EyeSoft.ActiveCampaign.Query.List.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class ListRequest : ActiveCampaignRequest
	{
		public ListRequest(string ids)
		{
			Ids = ids;
		}

		public string Full { get; set; }

		public string Ids { get; set; }
	}
}


