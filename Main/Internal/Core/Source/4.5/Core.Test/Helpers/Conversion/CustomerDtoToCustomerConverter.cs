namespace EyeSoft.Core.Test.Helpers.Conversion
{
	internal class CustomerDtoToCustomerConverter
		: IConverter<Customer, CustomerDto>
	{
		public CustomerDto Convert(Customer source)
		{
			return new CustomerDto { CustomerName = source.Name };
		}
	}
}