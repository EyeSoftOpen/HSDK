namespace EyeSoft.ActiveCampaign.Commanding.Contact.Models
{
	public class ContactDelete : ActiveCampaignRequest
	{
		public ContactDelete(string id)
		{
			Id = id;
		}

		public string Id { get; }
	}
}