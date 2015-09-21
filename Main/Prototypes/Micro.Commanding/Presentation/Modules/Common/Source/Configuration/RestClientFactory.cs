using EyeSoft.Web.Http.Client;

namespace EyeSoft.Architecture.Prototype.Windows.Configuration
{
	internal class RestClientFactory : IRestClientFactory
	{
		private readonly string baseAddress;

		public RestClientFactory(string baseAddress)
		{
			this.baseAddress = baseAddress;
		}

		public IRestClient Create()
		{
			return new RestClient(baseAddress);
		}
	}
}