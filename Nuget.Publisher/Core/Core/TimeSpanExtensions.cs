namespace EyeSoft.Nuget.Publisher.Core.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class TimeSpanExtensions
	{
		public static string ToHumanReadable(this TimeSpan timeSpan)
		{
			// formats and its cutoffs based on totalseconds
			var cutoff =
				new SortedList<long, string>
					{
						{ 60, "{3:S}" },
						{ 60 * 60, "{2:M} and {3:S}" },
						{ 24 * 60 * 60, "{1:H} and {2:M}" },
						{ long.MaxValue , "{0:D} and {1:H}" }
					};

			var find = cutoff.Keys.ToList().BinarySearch((long)timeSpan.TotalSeconds);

			var near = find < 0 ? Math.Abs(find) - 1 : find;

			return string.Format(
				new SingularPluralTimeFormatter(),
				cutoff[cutoff.Keys[near]],
				timeSpan.Days,
				timeSpan.Hours,
				timeSpan.Minutes,
				timeSpan.Seconds);
		}
	}
}