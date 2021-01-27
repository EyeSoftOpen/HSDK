namespace EyeSoft.Accounting.Italian.FiscalCode
{
    using System.Linq;

    internal static class StringExtensions
	{
		public static string EvenChars(this string word)
		{
			return word.FilterByPosition(true);
		}

		public static string OddChars(this string word)
		{
			return word.FilterByPosition(false);
		}

		private static string FilterByPosition(this string word, bool pair)
		{
			var pairPositionChars =
				word
					.Where((x, position) => (position % 2 == 0) == pair)
					.ToArray();

			var filterByPosition = new string(pairPositionChars);

			return filterByPosition;
		}
	}
}