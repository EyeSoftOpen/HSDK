namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EnforceTest
	{
		[TestMethod]
		public void VerifyNullParameterValidationThrowArgumentNullExceptionAndCheckParameterNameIsCorrect()
		{
			EnforceParameter(null);
		}

		[TestMethod]
		public void VerifyNullStringParameterValidationThrowArgumentNullExceptionAndCheckParameterNameIsCorrect()
		{
			EnforceParameter(string.Empty);
		}

		[TestMethod]
		public void VerifyBaseParameterValidationThrowArgumentNullExceptionAndCheckParameterNameIsCorrect()
		{
			EnforceParameter<IComparable>(Guid.Empty);
		}

		private void EnforceParameter(object obj)
		{
			EnforceParameter<object>(obj);
		}

		private void EnforceParameter<T>(T obj)
		{
			Executing
				.This(() => Enforce.Argument(() => obj))
				.Should().Throw<ArgumentNullException>()
				.And.Exception.ParamName.Should("The parameter name in the ArgumentNullException is not correct.")
				.Be.EqualTo("obj");
		}
	}
}