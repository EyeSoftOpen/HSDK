namespace EyeSoft.ActiveCampaign.Commanding.List
{
    using System;

    using EyeSoft.ActiveCampaign.Commanding.List.Models;
    using EyeSoft.ActiveCampaign.Query;

    public interface IListCommandingClient : IDisposable
	{
	    ActiveCampaignResponse Add(AddListCommand command);
	}
}