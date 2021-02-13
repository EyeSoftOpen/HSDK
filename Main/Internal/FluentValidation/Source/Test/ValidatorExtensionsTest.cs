namespace EyeSoft.FluentValidation.Test
{
    using EyeSoft.Validation;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ValidatorExtensionsTest
	{
		[TestMethod]
		public void CheckValidationExceptionWithNotValidInstance()
		{
			const string ExceptionMessage =
				"The instance of type 'ValidableCustomer' is not valid:" + "\r\n" +
				"\t- Name: Should not be empty. - Value: \r\n" +
				"\t- Address: Should not be empty. - Value:";

			Executing.This(() => new CustomerFluentValidator().ThrowOnErrors(new ValidatorTest.ValidableCustomer()))
				.Should().Throw<ValidationException>()
				.And
				.Exception.Message.Should().Be.EqualTo(ExceptionMessage);
		}
	}
}