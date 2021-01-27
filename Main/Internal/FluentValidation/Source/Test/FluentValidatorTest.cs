namespace EyeSoft.FluentValidation.Test
{
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class FluentValidatorTest
	{
		private readonly ValidatorTest validatorTest = new ValidatorTest(new CustomerFluentValidator());

		[TestMethod]
		public void FluentCheckValidPropertyExpectedNoErrors()
		{
			validatorTest.CheckValidPropertyExpectedNoErrors();
		}

		[TestMethod]
		public void FluentCheckNotValidPropertiesExpectedError()
		{
			validatorTest.CheckNotValidPropertiesExpectedError();
		}

		[TestMethod]
		public void FluentCheckNotValidPropertyExpectedError()
		{
			validatorTest.CheckNotValidPropertyExpectedError();
		}

		[TestMethod]
		public void FluentCheckNotValidPropertyExpectedSingleError()
		{
			validatorTest.CheckNotValidPropertyExpectedSingleError();
		}

		[TestMethod]
		public void FluentCheckValidPropertiesExpectedNoErrors()
		{
			validatorTest.CheckValidPropertiesExpectedNoErrors();
		}
	}
}