namespace EyeSoft.ActiveCampaign.Query.List
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.List.Models;

	public interface IListQueryClient : IDisposable
	{
		IEnumerable<List> GetAll();
	}
}