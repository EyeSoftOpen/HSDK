namespace EyeSoft.ActiveCampaign.Query.Automation
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.ActiveCampaign.Query.Automation.Models;

	public interface IAutomationQueryClient : IDisposable
	{
		IEnumerable<Automation> GetAll();
	}
}