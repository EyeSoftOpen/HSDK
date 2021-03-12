namespace EyeSoft.FluentValidation.Test
{
    using System;
    using EyeSoft.Validation;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

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

            Action action = () => new CustomerFluentValidator().ThrowOnErrors(new ValidatorTest.ValidableCustomer());
                
            action.Should().Throw<ValidationException>().And.Message.Should().Be(ExceptionMessage);
		}
	}
}