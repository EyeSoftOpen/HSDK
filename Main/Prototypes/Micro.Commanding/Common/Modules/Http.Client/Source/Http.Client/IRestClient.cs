namespace Model.ViewModels.Main.Persisters
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;

	public interface IRestClient : IDisposable
	{
		Task<HttpResponseMessage> PutAsJsonAsync<TCommand>(string requestUri, TCommand command);

		T Get<T>(string requestUri, Guid id);
	}
}