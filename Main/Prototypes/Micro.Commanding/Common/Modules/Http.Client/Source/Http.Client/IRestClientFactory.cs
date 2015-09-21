namespace EyeSoft.Web.Http.Client
{
	public interface IRestClientFactory
	{
		IRestClient Create();
	}
}