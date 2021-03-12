namespace EyeSoft.Core.Test.Conversion
{
    using Helpers;
    using Helpers.Conversion;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

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
				.Should().Be(KnownCustomer.Customer.Name);
		}

		[TestMethod]
		public void UseAConverterDirectly()
		{
			Converters
				.Types<Customer, CustomerDto>()
				.Converter<CustomerDtoToCustomerConverter>()
				.Convert(KnownCustomer.Customer)
				.CustomerName
				.Should().Be(KnownCustomer.Customer.Name);
		}
	}
}