namespace EyeSoft.ActiveCampaign.Shell.Helpers
{
	internal class ActiveCampaignTestData
	{
		public ActiveCampaignTestData(string account, string apiKey, int contactId, string contactEmail, int automation)
		{
			Account = account;
			ApiKey = apiKey;
			ContactId = contactId;
			ContactEmail = contactEmail;
			Automation = automation;
		}

		public string Account { get; }

		public string ApiKey { get; }

		public int ContactId { get; }

		public string ContactEmail { get; }

		public int Automation { get; }
	}
}