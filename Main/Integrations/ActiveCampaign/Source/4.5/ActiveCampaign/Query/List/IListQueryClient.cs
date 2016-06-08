namespace EyeSoft.ActiveCampaign.Query.List
{
	using System;

	using EyeSoft.ActiveCampaign.Query.List.Models;

	public interface IListQueryClient : IDisposable
	{
		Lists GetAllLists();
	}
}