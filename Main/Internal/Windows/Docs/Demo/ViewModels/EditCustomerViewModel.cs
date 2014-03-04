namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	using System.Diagnostics;

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

		public override string Result
		{
			get { return result; }
		}

		public override bool IsValid
		{
			get { return Customer.IsValid; }
		}

		protected override void Confirm()
		{
			result = "Calculated: " + Customer.FirstName.ToUpper();

			Close();
		}
	}
}