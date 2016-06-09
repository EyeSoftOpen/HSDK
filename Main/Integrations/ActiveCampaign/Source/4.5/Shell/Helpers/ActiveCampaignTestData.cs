namespace EyeSoft.ActiveCampaign.Shell.Helpers
{
	internal class ActiveCampaignTestData
	{
		public ActiveCampaignTestData(string account, string apiKey, string contactId, string contactEmail, string automation)
		{
			Account = account;
			ApiKey = apiKey;
			ContactId = contactId;
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string Account { get; }

		public string ApiKey { get; }

		public string ContactId { get; }

		public string ContactEmail { get; }

		public string Automation { get; }
	}
}