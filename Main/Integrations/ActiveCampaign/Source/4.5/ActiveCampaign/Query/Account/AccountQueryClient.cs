namespace EyeSoft.ActiveCampaign.Query.Account
{
	using EyeSoft.ActiveCampaign.Query.Account.Models;

	public class AccountQueryClient : ActiveCampaignRestClient, IAccountQueryClient
	{
		public AccountQueryClient(ActiveCampaignConnection connection)
			: base(connection)
		{
		}

		public AccountDetail GetAccount()
		{
			return ExecuteGetRequest<AccountDetail>("user_me", new UserMe());
		}
	}
}