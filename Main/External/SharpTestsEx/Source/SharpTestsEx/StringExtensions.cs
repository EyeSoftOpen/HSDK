using System;
using System.Collections.Generic;
using System.Text;

namespace SharpTestsEx
{
	public static class StringExtensions
	{
		public static string[] Lines(this string source)
		{
			return source.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
		}

		public static string AsCommaSeparatedValues(this IEnumerable<string> source)
		{
			if (source == null)
			{
				return string.Empty;
			}
			var result = new StringBuilder(100);
			bool appendComma = false;
			foreach (var value in source)
			{
				if (appendComma)
				{
					result.Append(", ");
				}
				result.Append(value);
				appendComma = true;
			}
			return result.ToString();
		}
	}
}