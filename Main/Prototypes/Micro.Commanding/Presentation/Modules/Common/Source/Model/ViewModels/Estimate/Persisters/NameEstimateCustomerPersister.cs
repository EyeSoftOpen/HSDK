using EyeSoft.Architecture.Prototype.Accounting.Commanding;
using EyeSoft.Architecture.Prototype.Windows.Model.Base;
using EyeSoft.Web.Http.Client;

namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate.Persisters
{
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