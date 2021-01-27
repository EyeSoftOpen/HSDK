namespace EyeSoft.Windows.Model.Demo.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Core.Validation;
    using EyeSoft.Windows.Model;
    using Model.ViewModels;

    [DebuggerDisplay("{Customer.FirstName} {Customer.LastName}")]
	public class EditCustomerViewModel : ConfirmCancelViewModel<string>
	{
		private string result;

		public EditCustomerViewModel(CustomerViewModel customer)
		{
			Customer = customer;
		}

		public CustomerViewModel Customer { get; private set; }

		public override string Result
		{
			get { return result; }
		}

		public override bool IsValid
		{
			get { return Customer.IsValid; }
		}

		public override IEnumerable<ValidationError> Validate()
		{
			return Customer.Validate();
		}

		protected override void Confirm()
		{
			result = "Calculated: " + Customer.FirstName.ToUpper();

			Close();
		}
	}
}