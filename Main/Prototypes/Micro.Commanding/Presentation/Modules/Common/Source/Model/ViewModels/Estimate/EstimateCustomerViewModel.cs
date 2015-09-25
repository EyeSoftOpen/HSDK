namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels.Estimate
{
	using System;
	using EyeSoft.Architecture.Prototype.Accounting.Commanding;
	using EyeSoft.Architecture.Prototype.Windows.Model.Base;
	using EyeSoft.Web.Http.Client;

	public class EstimateCustomerViewModel : PersisterViewModel<EstimateCustomerViewModel>
	{
		public EstimateCustomerViewModel(Guid id)
		{
			Id = id;

			Persisters
				.Add(x => x.Name, () => new UpdateCustomerNameCommand { Id = Guid.NewGuid(), EstimateId = Id, CustomerName = Name });

			Name = Resolver.Resolve<IRestClientFactory>().Create().Get<string>("estimate/getcustomername", Id);
		}

		public string Name
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}
	}
}