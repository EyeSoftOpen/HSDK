namespace Model.ViewModels.Main.Persisters
{
	public interface IRestClientFactory
	{
		IRestClient Create();
	}
}