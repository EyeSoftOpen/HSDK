namespace EyeSoft.ActiveCampaign.Commanding.List
{
    using EyeSoft.ActiveCampaign.Commanding.List.Models;
    using EyeSoft.ActiveCampaign.Query;

    public class ListCommandingClient : ActiveCampaignRestClient, IListCommandingClient
	{
		public ListCommandingClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public ActiveCampaignResponse Add(AddListCommand command)
		{
			return ExecutePostRequest<ActiveCampaignResponse>("list_add", command);
		}

		//public ActiveCampaignResponse Delete(int id)
		//{
		//	return ExecuteGetRequest<ActiveCampaignResponse>("contact_delete", new DeleteContactCommand(id));
		//}
    }
}