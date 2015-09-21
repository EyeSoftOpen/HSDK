namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;

	using EyeSoft.Web.Http.Client;

	internal class RestClient : IRestClient
	{
		private readonly HttpClient client;

		public RestClient(string baseAddress)
		{
			client = new HttpClient { BaseAddress = new Uri(baseAddress) };
		}

		public Task<HttpResponseMessage> PutAsJsonAsync<TCommand>(string requestUri, TCommand command)
		{
			return client.PutAsJsonAsync(requestUri, command);
		}

		public T Get<T>(string requestUri, Guid id)
		{
			var response = client.GetAsync($"{requestUri}/{id}").Result;

			////response.Verify();

			var result = response.Content.ReadAsAsync<T>();

			return result.Result;
		}

		public void Dispose()
		{
			client.Dispose();
		}
	}
}