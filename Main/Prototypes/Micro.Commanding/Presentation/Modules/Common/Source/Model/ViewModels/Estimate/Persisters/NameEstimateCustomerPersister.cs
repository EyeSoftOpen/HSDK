namespace Model.ViewModels.Main.Persisters
{
	using Commanding;

	public class NameEstimateCustomerPersister : Persister<UpdateCustomerNameCommand>
	{
		public NameEstimateCustomerPersister(IRestClientFactory restClientFactory) : base(restClientFactory)
		{
		}

		public override void Persist(UpdateCustomerNameCommand command)
		{
			PutAsJson("estimate/UpdateCustomerName", command);
		}
	}
}