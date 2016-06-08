namespace EyeSoft.ActiveCampaign
{
	using System;
	using System.Net;

	public class ActiveCampaignConnection : IDisposable
	{
		private readonly Lazy<WebClient> lazyWebClient;

		public ActiveCampaignConnection(string account, string apiKey)
		{
			if (string.IsNullOrEmpty(account))
			{
				throw new ArgumentException("The reseller or customer API URL was not specified", nameof(account));
			}

			if (string.IsNullOrEmpty(apiKey))
			{
				throw new ArgumentException("The API key was not specified", nameof(apiKey));
			}

			Account = account;
			ApiKey = apiKey;


			lazyWebClient = new Lazy<WebClient>(() => new WebClient());
		}

		public string Account { get; }

		public string ApiKey { get; }

		internal WebClient WebClient => lazyWebClient.Value;

		public void Dispose()
		{
			if (!lazyWebClient.IsValueCreated)
			{
				return;
			}

			lazyWebClient.Value.Dispose();
		}
	}
}