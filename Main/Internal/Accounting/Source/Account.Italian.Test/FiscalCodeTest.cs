using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EyeSoft.Accounting.Italian.Test
{
	using System;

	using EyeSoft.Accounting.Italian.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class FiscalCodeTest
	{
		[TestMethod]
		public void ValidateMaleFiscalCodeIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.MaleCode);
			fiscalCode.Control.Should().Be.EqualTo("Q");
			fiscalCode.BirthDate.Should().Be.EqualTo(new DateTime(1960, 1, 1));
			fiscalCode.IsValid.Should().Be.True();
		}

		[TestMethod]
		public void ValidateMaleFiscalCodeWithSpaceInNameIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.MaleWothSpaceInNameCode);
			fiscalCode.Control.Should().Be.EqualTo("S");
			fiscalCode.BirthDate.Should().Be.EqualTo(new DateTime(1960, 1, 1));
			fiscalCode.IsValid.Should().Be.True();
		}

		[TestMethod]
		public void ValidateFemaleFiscalCodeIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.FemaleCode);
			fiscalCode.Control.Should().Be.EqualTo("W");
			fiscalCode.BirthDate.Should().Be.EqualTo(new DateTime(1992, 8, 20));
			fiscalCode.IsValid.Should().Be.True();
		}

		[TestMethod]
		public void RevertNotValidFiscalCode()
		{
			var wrongFiscalCode = 'U' + Known.FiscalCodes.FemaleCode.Substring(1);
			var revertFiscalCode = new RevertedFiscalCode(wrongFiscalCode);
			revertFiscalCode.IsValid.Should().Be.False();
			revertFiscalCode.BirthDate.HasValue.Should().Be.False();
		}

		[TestMethod]
		public void RevertLowerValidFiscalCode()
		{
			var lowerFiscalCode = Known.FiscalCodes.FemaleCode.ToLower();
			var revertFiscalCode = new RevertedFiscalCode(lowerFiscalCode);
			revertFiscalCode.IsValid.Should().Be.True();
			revertFiscalCode.BirthDate.HasValue.Should().Be.True();
		}
	}
}
