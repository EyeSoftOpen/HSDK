namespace Model.ViewModels.Main.Persisters
{
	using Commanding;

	public class DescriptionEstimatePersister : Persister<UpdateEstimateDescriptionCommand>
	{
		public DescriptionEstimatePersister(IRestClientFactory restClientFactory) : base(restClientFactory)
		{
		}

		public override void Persist(UpdateEstimateDescriptionCommand command)
		{
			PutAsJson("Estimate/UpdateDescription", command);
		}
	}
}