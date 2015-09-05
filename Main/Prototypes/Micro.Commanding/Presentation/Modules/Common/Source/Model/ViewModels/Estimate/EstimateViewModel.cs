namespace Model
{
	using System;
	using System.Collections.Generic;

	using Commanding;

	using EyeSoft.Validation;

	using Model.ViewModels.Main.Persisters;

	public class EstimateViewModel : PersisterViewModel<EstimateViewModel>
	{
		public EstimateViewModel()
		{
			Id = new Guid("E36088B0-3A93-40E4-99F7-0A2CA0586E18");

			SuspendPropertyChanged();

			Persisters.Add(x => x.Description, () => new UpdateEstimateDescriptionCommand { Id = Guid.NewGuid(), EstimateId = Id, Description = Description });

			Description = Resolver.Resolve<IRestClientFactory>().Create().Get<string>("estimate/getdescription", Id);

			Customer = new EstimateCustomerViewModel(Id);

			ResumePropertyChanged();
		}

		public string Description
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public EstimateCustomerViewModel Customer
		{
			get;
		}

		protected override IEnumerable<ValidationError> Validate()
		{
			return new EstimateViewModelValidator().Validate(this);
		}
	}
}