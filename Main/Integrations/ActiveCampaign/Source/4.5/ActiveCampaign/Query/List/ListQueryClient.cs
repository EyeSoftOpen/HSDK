namespace EyeSoft.ActiveCampaign.Query.List
{
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.List.Models;

	public class ListQueryClient : ActiveCampaignRestClient, IListQueryClient
	{
		public ListQueryClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public IEnumerable<List> GetAll()
		{
			var request = new ListRequest("all");

			return ExecuteGetRequest<Lists>("list_list", request).Data;
		}
	}
}