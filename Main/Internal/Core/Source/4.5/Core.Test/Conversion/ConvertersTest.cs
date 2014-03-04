namespace EyeSoft.Test.Conversion
{
	using EyeSoft.Test.Helpers;
	using EyeSoft.Test.Helpers.Conversion;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ConvertersTest
	{
		[TestMethod]
		public void UseAConverterAfterRegistration()
		{
			Converters
				.Types<Customer, CustomerDto>()
				.Converter<CustomerDtoToCustomerConverter>();

			Converters
				.From<Customer>()
				.To<CustomerDto>(KnownCustomer.Customer)
				.CustomerName
				.Should().Be.EqualTo(KnownCustomer.Customer.Name);
		}

		[TestMethod]
		public void UseAConverterDirectly()
		{
			Converters
				.Types<Customer, CustomerDto>()
				.Converter<CustomerDtoToCustomerConverter>()
				.Convert(KnownCustomer.Customer)
				.CustomerName
				.Should().Be.EqualTo(KnownCustomer.Customer.Name);
		}
	}
}