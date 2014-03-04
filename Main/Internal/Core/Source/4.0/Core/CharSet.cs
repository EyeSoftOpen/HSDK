namespace EyeSoft
{
	using System.Collections.Generic;
	using System.Linq;

	public static class CharSet
	{
		public static readonly IEnumerable<char> LowerCase = Set('a', 'z');

		public static readonly IEnumerable<char> UpperCase = LowerCase.Select(char.ToUpper);

		public static readonly IEnumerable<char> NotRoman = new[] { 'x', 'y', 'k', 'j' };

		public static readonly IEnumerable<char> Symbols = new[] { '.', '?', '_', '-' };

		public static readonly IEnumerable<char> SimplifiedLowerCase = LowerCase.Except(NotRoman);

		public static readonly IEnumerable<char> SimplifiedUpperCase = UpperCase.Except(NotRoman);

		public static readonly IEnumerable<char> Numbers = Set('0', '9');

		public static readonly IEnumerable<IEnumerable<char>> Complex =
			new[] { SimplifiedLowerCase, SimplifiedUpperCase, Numbers, Symbols };

		private static IEnumerable<char> Set(char start, char end)
		{
			return
				Enumerable.Range(start, end - start + 1).Select(c => (char)c);
		}
	}
}