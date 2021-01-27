namespace EyeSoft.Accounting.Italian.Test.Helpers
{
	using System;
    using FiscalCode;

    internal static class Known
	{
		public static class FiscalCodes
		{
			public const string RomaAreaCode = "H501";

			public const string FirenzeAreaCode = "D612";

			public const string MaleCode =
				"RSSMRA60A01" + RomaAreaCode + "Q";

			public const string FemaleCode =
				"BNCCHR92M60" + FirenzeAreaCode + "W";

			public const string MaleWothSpaceInNameCode =
				"RSSMRC60A01" + RomaAreaCode + "S";

			public static readonly FiscalCode Male =
				new FiscalCode("RSS", "MRA", "60", "A", "01", RomaAreaCode, "Q");

			public static readonly FiscalCode Female =
				new FiscalCode("BNC", "CHR", "92", "M", "60", FirenzeAreaCode, "W");

			public static readonly FiscalCode MaleWithAccent =
				new FiscalCode("VRD", "MSO", "60", "A", "01", RomaAreaCode, "E");

			public static readonly FiscalCode MaleWothSpaceInName =
				new FiscalCode("RSS", "MRC", "60", "A", "01", RomaAreaCode, "S");
		}

		public static class NaturalPersons
		{
			public static readonly NaturalPerson Male =
				new NaturalPerson("Mario", "Rossi", new DateTime(1960, 1, 1), Sex.Male);

			public static readonly NaturalPerson MaleWithSpaceInName =
				new NaturalPerson("Mario Caio", "Rossi", new DateTime(1960, 1, 1), Sex.Male);

			public static readonly NaturalPerson Female =
				new NaturalPerson("Chiara", "Bianchi", new DateTime(1992, 8, 20), Sex.Female);

			public static readonly NaturalPerson MaleWithAccent =
				new NaturalPerson("Mosè", "Verdi", new DateTime(1960, 1, 1), Sex.Male);
		}
	}
}