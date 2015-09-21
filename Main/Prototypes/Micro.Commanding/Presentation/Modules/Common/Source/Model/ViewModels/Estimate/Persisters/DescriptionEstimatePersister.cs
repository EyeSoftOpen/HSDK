using EyeSoft.Architecture.Prototype.Accounting.Commanding;
using EyeSoft.Architecture.Prototype.Windows.Model.Base;
using EyeSoft.Web.Http.Client;

namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate.Persisters
{
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