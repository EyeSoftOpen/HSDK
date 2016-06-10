namespace EyeSoft.ActiveCampaign.Query.Contact.Models
{
	using EyeSoft.ActiveCampaign.Commanding;

	public class ContactViewEmailRequest : ActiveCampaignRequest
	{
		public ContactViewEmailRequest(string email)
		{
			Email = email;
		}

		public string Email { get; }
	}
}