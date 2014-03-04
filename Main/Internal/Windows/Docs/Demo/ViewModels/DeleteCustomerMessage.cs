namespace EyeSoft.Windows.Model.Demo.ViewModels
{
	public class DeleteCustomerMessage
	{
		public DeleteCustomerMessage(CustomerViewModel customer)
		{
			Customer = customer;
		}

		public CustomerViewModel Customer { get; private set; }
	}
}