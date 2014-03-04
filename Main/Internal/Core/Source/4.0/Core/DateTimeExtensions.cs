namespace EyeSoft
{
	using System;
	using System.Globalization;

	public static class DateTimeExtensions
	{
		public static string FullTime(this DateTime date)
		{
			var now = date;

			return string.Format("{0} {1}", now.ToShortDateString(), now.ToShortTimeString());
		}

		public static string FullSystemTime(this DateTime date)
		{
			return date.ToString("yyyy_MM_dd.HH_mm");
		}

		public static DateTime LastDay(this DateTime date, DayOfWeek dayOfWeek)
		{
			var day = LastDayOfMonth(date);

			while (day.DayOfWeek != dayOfWeek)
			{
				day = day.AddDays(-1);
			}

			return day;
		}

		public static string ToInvariantCultureDateString(this DateTime dateTime)
		{
			return dateTime.ToString("ddMMyyyy");
		}

		public static DateTime FirstDayOfWeek(this DateTime dateTime)
		{
			return dateTime.FirstDayOfWeek(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		}

		public static DateTime LastDayOfWeek(this DateTime dateTime)
		{
			return dateTime.LastDayOfWeek(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		}

		public static DateTime LastDayOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
		{
			return dateTime.FirstDayOfWeek().AddDays(6);
		}

		public static DateTime FirstDayOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
		{
			var diff = dateTime.DayOfWeek - firstDayOfWeek;

			if (diff < 0)
			{
				diff += 7;
			}

			var day = dateTime.AddDays(-1 * diff).Date;

			return day;
		}

		public static DateTime FirstDayOfMonth(this DateTime date)
		{
			return date.AddDays(-date.Day + 1);
		}

		public static DateTime LastDayOfMonth(this DateTime date)
		{
			var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

			return date.AddDays(daysInMonth - date.Day);
		}
	}
}