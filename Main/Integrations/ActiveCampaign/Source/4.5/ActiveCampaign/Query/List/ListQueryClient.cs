namespace EyeSoft.ActiveCampaign.Query.List
{
	using EyeSoft.ActiveCampaign.Query.List.Models;

	public class ListQueryClient : ActiveCampaignRestClient, IListQueryClient
	{
		public ListQueryClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public Lists GetAllLists()
		{
			var request = new ListRequest("all");

			return ExecuteGetRequest<Lists>("list_list", request);
		}
	}
}