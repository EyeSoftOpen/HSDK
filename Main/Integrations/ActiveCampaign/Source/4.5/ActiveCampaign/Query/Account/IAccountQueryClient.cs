namespace EyeSoft.ActiveCampaign.Query.Account
{
	using System;

	using EyeSoft.ActiveCampaign.Query.Account.Models;

	public interface IAccountQueryClient : IDisposable
	{
		AccountDetail GetAccount();
	}
}