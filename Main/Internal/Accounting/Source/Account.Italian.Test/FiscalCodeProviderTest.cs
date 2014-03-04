namespace EyeSoft.Accounting.Italian.Test
{
	using EyeSoft.Accounting.Italian.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

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

			fiscalCode.LastName.Should().Be.EqualTo(expected.LastName);
			fiscalCode.FirstName.Should().Be.EqualTo(expected.FirstName);
			fiscalCode.Year.Should().Be.EqualTo(expected.Year);
			fiscalCode.Month.Should().Be.EqualTo(expected.Month);
			fiscalCode.Day.Should().Be.EqualTo(expected.Day);
			fiscalCode.Area.Should().Be.EqualTo(expected.Area);
			fiscalCode.Control.Should().Be.EqualTo(expected.Control);

			fiscalCode.Should().Be.EqualTo(expected);

			fiscalCode.IsValid.Should().Be.True();
		}
	}
}
