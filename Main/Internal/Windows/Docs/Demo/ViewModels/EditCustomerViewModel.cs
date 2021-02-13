namespace EyeSoft.Windows.Model.Demo.ViewModels
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using EyeSoft.Validation;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model;

    [DebuggerDisplay("{Customer.FirstName} {Customer.LastName}")]
	public class EditCustomerViewModel : ConfirmCancelViewModel<string>
	{
		private string result;

		public EditCustomerViewModel(CustomerViewModel customer)
		{
			Customer = customer;
		}

		public CustomerViewModel Customer { get; private set; }

		public override string Result => result;

        public override bool IsValid => Customer.IsValid;

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