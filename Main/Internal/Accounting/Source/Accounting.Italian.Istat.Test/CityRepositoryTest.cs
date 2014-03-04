namespace Accounting.Italian.Istat.Test
{
	using System.Linq;

	using EyeSoft.Accounting.Italian.Istat;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class CityRepositoryTest
	{
		[TestMethod]
		public void VerifyItalianCitiesAreTheCorrectNumber()
		{
			new CityRepository().AllCity()
				.Count().Should().Be.EqualTo(110);
		}

		[TestMethod]
		public void VerifyItalianTownsAreTheCorrectNumber()
		{
			new CityRepository().AllTown()
				.Count().Should().Be.EqualTo(8093);
		}

		[TestMethod]
		public void VerifyItalianTownsByCityAndStartIsCorrect()
		{
			new CityRepository().TownsInCity("Verona")
				.Count().Should().Be.EqualTo(98);
		}

		[TestMethod]
		public void VerifyItalianTownsWithAccentByCityAndStartIsCorrect()
		{
			new CityRepository().TownsContains("Nardò").Should().Have.Count.EqualTo(1);
		}

		[TestMethod]
		public void VerifyItalianTownsByNametIsCorrect()
		{
			new CityRepository().TownsByName("Roma").Single()
				.Area.Should().Be.EqualTo("H501");
		}

		[TestMethod]
		public void VerifyItalianAreaCodeByTownCityAndNameIsCorrect()
		{
			new CityRepository().TownsInCity("Verona", "Verona")
				.Single().Area.Should().Be.EqualTo("L781");
		}
	}
}