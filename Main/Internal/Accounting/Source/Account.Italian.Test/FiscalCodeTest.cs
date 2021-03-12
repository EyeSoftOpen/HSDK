namespace EyeSoft.Accounting.Italian.Test
{
	using System;

	using EyeSoft.Accounting.Italian.Test.Helpers;
    using FiscalCode;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	[TestClass]
	public class FiscalCodeTest
	{
		[TestMethod]
		public void ValidateMaleFiscalCodeIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.MaleCode);
			fiscalCode.Control.Should().Be("Q");
			fiscalCode.BirthDate.Should().Be(new DateTime(1960, 1, 1));
			fiscalCode.IsValid.Should().BeTrue();
		}

		[TestMethod]
		public void ValidateMaleFiscalCodeWithSpaceInNameIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.MaleWothSpaceInNameCode);
			fiscalCode.Control.Should().Be("S");
			fiscalCode.BirthDate.Should().Be(new DateTime(1960, 1, 1));
			fiscalCode.IsValid.Should().BeTrue();
		}

		[TestMethod]
		public void ValidateFemaleFiscalCodeIsCorrect()
		{
			var fiscalCode = new RevertedFiscalCode(Known.FiscalCodes.FemaleCode);
			fiscalCode.Control.Should().Be("W");
			fiscalCode.BirthDate.Should().Be(new DateTime(1992, 8, 20));
			fiscalCode.IsValid.Should().BeTrue();
		}

		[TestMethod]
		public void RevertNotValidFiscalCode()
		{
			var wrongFiscalCode = 'U' + Known.FiscalCodes.FemaleCode.Substring(1);
			var revertFiscalCode = new RevertedFiscalCode(wrongFiscalCode);
			revertFiscalCode.IsValid.Should().BeFalse();
			revertFiscalCode.BirthDate.HasValue.Should().BeFalse();
		}

		[TestMethod]
		public void RevertLowerValidFiscalCode()
		{
			var lowerFiscalCode = Known.FiscalCodes.FemaleCode.ToLower();
			var revertFiscalCode = new RevertedFiscalCode(lowerFiscalCode);
			revertFiscalCode.IsValid.Should().BeTrue();
			revertFiscalCode.BirthDate.HasValue.Should().BeTrue();
		}
	}
}
