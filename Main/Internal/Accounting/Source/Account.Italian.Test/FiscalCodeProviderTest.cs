namespace EyeSoft.Accounting.Italian.Test
{
	using EyeSoft.Accounting.Italian.Test.Helpers;
    using FiscalCode;
    using FiscalCode.Parts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	[TestClass]
	public class FiscalCodeProviderTest
	{
		private readonly FiscalCodeProvider provider = new FiscalCodeProvider();

		[TestMethod]
		public void CheckMaleFiscalCodeIsCorrect()
		{
			Check(Known.NaturalPersons.Male, Known.FiscalCodes.RomaAreaCode, Known.FiscalCodes.Male);
		}

		[TestMethod]
		public void CheckMaleFiscalCodeWithSpaceInNameIsCorrect()
		{
			Check(Known.NaturalPersons.MaleWithSpaceInName, Known.FiscalCodes.RomaAreaCode, Known.FiscalCodes.MaleWothSpaceInName);
		}

		[TestMethod]
		public void CheckMaleFiscalWithAccentCodeIsCorrect()
		{
			Check(Known.NaturalPersons.MaleWithAccent, Known.FiscalCodes.RomaAreaCode, Known.FiscalCodes.MaleWithAccent);
		}

		[TestMethod]
		public void CheckFemaleFiscalCodeIsCorrect()
		{
			Check(Known.NaturalPersons.Female, Known.FiscalCodes.FirenzeAreaCode, Known.FiscalCodes.Female);
		}

		private void Check(NaturalPerson person, string areaCode, CalculatedFiscalCode expected)
		{
			var fiscalCode = provider.Calculate(person, new AreaCode(areaCode));

			fiscalCode.LastName.Should().Be(expected.LastName);
			fiscalCode.FirstName.Should().Be(expected.FirstName);
			fiscalCode.Year.Should().Be(expected.Year);
			fiscalCode.Month.Should().Be(expected.Month);
			fiscalCode.Day.Should().Be(expected.Day);
			fiscalCode.Area.Should().Be(expected.Area);
			fiscalCode.Control.Should().Be(expected.Control);

			fiscalCode.Should().Be(expected);

			fiscalCode.IsValid.Should().BeTrue();
		}
	}
}
