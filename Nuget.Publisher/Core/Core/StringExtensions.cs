namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.Linq;

	public static class StringExtensions
	{
		public static string SplitWordsFirstWordCapital(this string text)
		{
			var words = new List<string>();

			var current = string.Empty;

			foreach (var c in text)
			{
				if (char.IsUpper(c))
				{
					words.Add(current);

					current = new string(new[] { c });
				}
				else
				{
					current += c;
				}
			}

			words.Add(current);

			var splitWordsFirstWordCapital = string.Join(
				" ",
				words.Where(x => !string.IsNullOrWhiteSpace(x))
					.Select((x, index) => new { Index = index, Word = x })
					.Select(x => x.Index > 0 ? x.Word.ToLower() : x.Word));

			return splitWordsFirstWordCapital;
		}
	}
}
