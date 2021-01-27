namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Windows;
	using System.Windows.Input;
    using Core.Messanging;
    using Core.Timers;
    using Core.Validation;
    using DialogService;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.Collections.ObjectModel;
	using EyeSoft.Windows.Model.Demo.Contract;
	using EyeSoft.Windows.Model.Demo.ViewModels.Validators;
    using Model.ViewModels;
    using ServiceProxy;
    using ServiceProxy.Collection.Property;

    public class MainViewModel : BaseViewModel
	{
		private readonly ServiceFactory<ICustomerService> serviceFactory;

		private readonly ITimerFactory timerFactory;

		public MainViewModel()
		{
			serviceFactory = ViewModelController.ServiceFactory<ICustomerService>();
			timerFactory = ViewModelController.TimerFactory();

			MessageBroker.Register<DeleteCustomerMessage>(this, DeleteCustomer);

			Property(() => FullName).OnChanged(x => NameLength = x.Length);

			InitializeProperties();
		}

		~MainViewModel()
		{
			Dispose();
		}

		public string FullName
		{
			get => GetProperty<string>();
            set => SetProperty(value);
        }

		public int NameLength
		{
			get => GetProperty<int>();
            private set => SetProperty(value);
        }

		public CustomerViewModel MainCustomer
		{
			get => GetProperty<CustomerViewModel>();
            set => SetProperty(value);
        }

		public IObservableCollection<CustomerViewModel> CustomerCollection { get; set; }

		public ICommand ReloadCommand { get; private set; }

		public override bool CanClose()
		{
			if (!IsValid)
			{
				DialogService.ShowMessage("Data not valid", "The data must be valid.");
			}

			return base.CanClose();
		}

		protected override void ShowChild()
		{
			Thread.Sleep(2000);

			var nameTokens = FullName.Trim().Split();
			var editCustomerViewModel = new EditCustomerViewModel(new CustomerViewModel(nameTokens[0], nameTokens[1]));
			var result = DialogService.ShowModal<EditCustomerViewModel, string>(editCustomerViewModel);

			DialogService.ShowMessage("Window closed", "Value returned:\r\n" + result);
		}

		protected bool CanShowChild()
		{
			return !string.IsNullOrWhiteSpace(FullName) && IsValid;
		}

		public override IEnumerable<ValidationError> Validate()
		{
			return new MainViewModelValidator().Validate(this);
		}

		protected void Reload()
		{
			LoadFromService();
		}

		private void InitializeProperties()
		{
			LoadFromService().Completed(collection =>
				{
					collection.Add(new CustomerViewModel("Added after", "Completed"));

					SimulateEditingAndUpdateAnItem(collection);
				});

			serviceFactory
				.Property(this, x => x.MainCustomer)
				.Fill(x => x.GetMainCustomer())
				.Completed(
					x =>
						{
							if (x == null)
							{
								return;
							}

							x.Visits++;

							serviceFactory
								.With(MainCustomer)
								.Execute<CustomerDto>((service, entity) => service.Save(entity))
								.Completed(viewModel => DialogService.ShowMessage("Saved", "Save completed"));
						});
		}

		private ICollectionPropertyFilled<CustomerViewModel> LoadFromService()
		{
			return
				serviceFactory
					.Collection(this, x => x.CustomerCollection)
					.Sort((x, y) => string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal))
					.Fill(x => x.GetCustomersWithTurnoverGreatherThan(1000));
		}

		private void SimulateEditingAndUpdateAnItem(IObservableCollection<CustomerViewModel> collection)
		{
			Action action =
				() =>
				{
					if (collection.Count == 0)
					{
						return;
					}

					var index = collection.Count - 1;
					var item = collection[index];
					item = new CustomerViewModel(item.FirstName.ToUpper(), item.LastName);

					serviceFactory
						.With(item)
						.Execute<CustomerDto>((service, entity) => service.Save(entity))
						.UpdateCollection(collection);

					timerFactory.DelayedExecute(2000, () => CustomerCollection.InvertSort());
				};

			timerFactory.DelayedExecute(3000, action);
		}

		private void DeleteCustomer(DeleteCustomerMessage deleteCustomerMessage)
		{
			var confirmDeleteCustomer = DialogService.ShowMessage("Delete customer", "Confirm deletion?", MessageBoxButton.YesNo);

			if (confirmDeleteCustomer == MessageBoxResult.No)
			{
				return;
			}

			serviceFactory.Execute(service => service.Delete(deleteCustomerMessage.Customer.Id));
			CustomerCollection.Remove(deleteCustomerMessage.Customer);
		}
	}
}