namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Windows.Input;
    using Core.Messanging;
    using Core.Validation;
    using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Demo.ViewModels.Validators;
    using Model.ViewModels;

    [DebuggerDisplay("{FirstName} {LastName}")]
	public class CustomerViewModel : IdentityViewModel
	{
		public CustomerViewModel(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;

			Property(() => FullName).DependsFrom(() => FirstName, () => LastName, () => Visits);
		}

		public ICommand DeleteCommand { get; private set; }

		public string FirstName
		{
			get => GetProperty<string>();
            set => SetProperty(value);
        }

		public string LastName
		{
			get => GetProperty<string>();
            set => SetProperty(value);
        }

		public string FullName => $"{FirstName} {LastName}";

		public int Visits
		{
			get => GetProperty<int>();
            set => SetProperty(value);
        }

		public DateTime? Visited
		{
			get => GetProperty<DateTime?>();
            set => SetProperty(value);
        }

		public override IEnumerable<ValidationError> Validate()
		{
			return new CustomerViewModelValidator().Validate(this);
		}

		protected void Delete()
		{
			MessageBroker.Send(new DeleteCustomerMessage(this));
		}
	}
}