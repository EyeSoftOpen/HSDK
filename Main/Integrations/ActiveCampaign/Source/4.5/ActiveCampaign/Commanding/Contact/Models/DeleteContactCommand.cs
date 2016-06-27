namespace EyeSoft.ActiveCampaign.Commanding.Contact.Models
{
	public class DeleteContactCommand : ActiveCampaignRequest
	{
		public DeleteContactCommand(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}
}