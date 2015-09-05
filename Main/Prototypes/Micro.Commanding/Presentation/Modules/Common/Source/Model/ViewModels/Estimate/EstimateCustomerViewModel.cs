namespace Model
{
	using System;

	using Commanding;

	using Model.ViewModels.Main.Persisters;

	public class EstimateCustomerViewModel : PersisterViewModel<EstimateCustomerViewModel>
	{
		public EstimateCustomerViewModel(Guid id)
		{
			Id = id;

			Persisters.Add(x => x.Name, () => new UpdateCustomerNameCommand { Id = Guid.NewGuid(), EstimateId = Id, CustomerName = Name });

			Name = Resolver.Resolve<IRestClientFactory>().Create().Get<string>("estimate/getcustomername", Id);
		}

		public string Name
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}
	}
}