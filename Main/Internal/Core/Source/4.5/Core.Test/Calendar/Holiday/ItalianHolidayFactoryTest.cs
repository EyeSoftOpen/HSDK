namespace EyeSoft.Test.Calendar
{
	using System;

	using EyeSoft.Calendar;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ItalianHolidayFactoryTest
	{
		private const string Carnevale = "Carnevale";
		private const string Capodanno = "Capodanno";
		private const string Epifania = "Epifania";
		private const string Pasqua = "Pasqua";
		private const string LunediDellAngelo = "Lunedi dell'angelo";
		private const string Liberazione = "Liberazione";
		private const string FestaDelLavoro = "Festa del lavoro";
		private const string FestaDellaRepubblica = "Festa della repubblica";
		private const string Ferragosto = "Ferragosto";
		private const string Ognissanti = "Ognissanti";
		private const string ImmacolataConcezione = "Immacolata concezione";
		private const string Natale = "Natale";
		private const string SantoStefano = "Santo Stefano";

		[TestMethod]
		public void AddItalianHolidaysFrom2011To2012YearsVerifyAreCorrect()
		{
			var expected =
				new[]
					{
						Holiday.Fixed(Capodanno, new DateTime(2011, 1, 1)),
						Holiday.Fixed(Epifania, new DateTime(2011, 1, 6)),
						Holiday.Entry(Carnevale, new DateTime(2011, 3, 6)),
						Holiday.Entry(Pasqua, new DateTime(2011, 4, 24)),
						Holiday.Entry(LunediDellAngelo, new DateTime(2011, 4, 25)),
						Holiday.Fixed(Liberazione, new DateTime(2011, 4, 25)),
						Holiday.Fixed(FestaDelLavoro, new DateTime(2011, 5, 1)),
						Holiday.Fixed(FestaDellaRepubblica, new DateTime(2011, 6, 2)),
						Holiday.Fixed(Ferragosto, new DateTime(2011, 8, 15)),
						Holiday.Fixed(Ognissanti, new DateTime(2011, 11, 1)),
						Holiday.Fixed(ImmacolataConcezione, new DateTime(2011, 12, 08)),
						Holiday.Fixed(Natale, new DateTime(2011, 12, 25)),
						Holiday.Fixed(SantoStefano, new DateTime(2011, 12, 26)),

						Holiday.Fixed(Capodanno, new DateTime(2012, 1, 1)),
						Holiday.Fixed(Epifania, new DateTime(2012, 1, 6)),
						Holiday.Entry(Carnevale, new DateTime(2012, 2, 21)),
						Holiday.Entry(Pasqua, new DateTime(2012, 4, 8)),
						Holiday.Entry(LunediDellAngelo, new DateTime(2012, 4, 9)),
						Holiday.Fixed(Liberazione, new DateTime(2012, 4, 25)),
						Holiday.Fixed(FestaDelLavoro, new DateTime(2012, 5, 1)),
						Holiday.Fixed(FestaDellaRepubblica, new DateTime(2012, 6, 2)),
						Holiday.Fixed(Ferragosto, new DateTime(2012, 8, 15)),
						Holiday.Fixed(Ognissanti, new DateTime(2012, 11, 1)),
						Holiday.Fixed(ImmacolataConcezione, new DateTime(2012, 12, 08)),
						Holiday.Fixed(Natale, new DateTime(2012, 12, 25)),
						Holiday.Fixed(SantoStefano, new DateTime(2012, 12, 26)),

						Holiday.Fixed(Capodanno, new DateTime(2013, 1, 1)),
						Holiday.Fixed(Epifania, new DateTime(2013, 1, 6)),
						Holiday.Entry(Carnevale, new DateTime(2013, 2, 12)),
						Holiday.Entry(Pasqua, new DateTime(2013, 3, 31)),
						Holiday.Entry(LunediDellAngelo, new DateTime(2013, 4, 1)),
						Holiday.Fixed(Liberazione, new DateTime(2013, 4, 25)),
						Holiday.Fixed(FestaDelLavoro, new DateTime(2013, 5, 1)),
						Holiday.Fixed(FestaDellaRepubblica, new DateTime(2013, 6, 2)),
						Holiday.Fixed(Ferragosto, new DateTime(2013, 8, 15)),
						Holiday.Fixed(Ognissanti, new DateTime(2013, 11, 1)),
						Holiday.Fixed(ImmacolataConcezione, new DateTime(2013, 12, 8)),
						Holiday.Fixed(Natale, new DateTime(2013, 12, 25)),
						Holiday.Fixed(SantoStefano, new DateTime(2013, 12, 26))
					};

			new HolidayFactory(2011, 2)
				.Fixed(Capodanno, new AgnosticDay(1, 1))
				.Fixed(Epifania, new AgnosticDay(6, 1))
				.Entry(Carnevale, new AgnosticDay(6, 3), new AgnosticDay(21, 2), new AgnosticDay(12, 2))
				.Entry(Pasqua, new AgnosticDay(24, 4), new AgnosticDay(8, 4), new AgnosticDay(31, 3))
				.Entry(LunediDellAngelo, new AgnosticDay(25, 4), new AgnosticDay(9, 4), new AgnosticDay(1, 4))
				.Fixed(Liberazione, new AgnosticDay(25, 4))
				.Fixed(FestaDelLavoro, new AgnosticDay(1, 5))
				.Fixed(FestaDellaRepubblica, new AgnosticDay(2, 6))
				.Fixed(Ferragosto, new AgnosticDay(15, 8))
				.Fixed(Ognissanti, new AgnosticDay(1, 11))
				.Fixed(ImmacolataConcezione, new AgnosticDay(8, 12))
				.Fixed(Natale, new AgnosticDay(25, 12))
				.Fixed(SantoStefano, new AgnosticDay(26, 12))
				.List()
				.Should().Have.SameSequenceAs(expected);
		}
	}
}