namespace EyeSoft.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class Scheduling
	{
		private static readonly IDictionary<RecurrencyFrequency, Func<DateTime, DateTime>> timeSpanToAddBasedOnFrequency =
				new Dictionary<RecurrencyFrequency, Func<DateTime, DateTime>>
			{
				{ RecurrencyFrequency.None, current => current },
				{ RecurrencyFrequency.Daily, current => current.AddDays(1) },
				{ RecurrencyFrequency.Weekly, current => current.AddDays(7) },
				{ RecurrencyFrequency.Biweekly, current => current.AddDays(14) },
				{ RecurrencyFrequency.Monthly, current => current.AddMonths(1) },
				{ RecurrencyFrequency.Yearly, current => current.AddYears(1) }
			};

		public static DateTime NextDate(this DateTime date, RecurrencyFrequency frequency)
		{
			var addTimeSpan = timeSpanToAddBasedOnFrequency[frequency];

			return addTimeSpan(date);
		}

		public static IEnumerable<DateTime> RecurrencyDays(
			RecurrencyFrequency frequency, DateTime start, DateTime end, DateTime startEventDate, DateTime? endEventDate = null)
		{
			var startRecurrencyDate = StartRecurrencyDate(frequency, start, startEventDate);

			if (!startRecurrencyDate.HasValue)
			{
				return Enumerable.Empty<DateTime>();
			}

			if (frequency == RecurrencyFrequency.None)
			{
				return new[] { startRecurrencyDate.Value };
			}

			IList<DateTime> recurrencies = new List<DateTime>();

			var endRangeDate = endEventDate.HasValue ? Comparer.Min(end, endEventDate) : end;

			while (startRecurrencyDate <= endRangeDate)
			{
				recurrencies.Add(startRecurrencyDate.Value);

				startRecurrencyDate = NextDate(startRecurrencyDate.Value, frequency);
			}

			return recurrencies;
		}

		public static DateTime? StartRecurrencyDate(RecurrencyFrequency recurrency, DateTime start, DateTime eventDate)
		{
			if (eventDate >= start)
			{
				return eventDate;
			}

			DateTime? startRecurrencyDate;

			switch (recurrency)
			{
				case RecurrencyFrequency.None:
					return null;

				case RecurrencyFrequency.Daily:
					return start;

				case RecurrencyFrequency.Weekly:
					startRecurrencyDate = start.FirstDayOfWeek(eventDate.DayOfWeek);
					break;

				case RecurrencyFrequency.Biweekly:
					startRecurrencyDate = start.FirstDayOfWeek(eventDate.DayOfWeek);
					var deltaDays = start.Subtract(eventDate).TotalDays.RoundToInt();
					startRecurrencyDate = startRecurrencyDate.Value.AddDays(14 - deltaDays);
					break;

				case RecurrencyFrequency.Monthly:
					startRecurrencyDate = new DateTime(start.Year, eventDate.Month, eventDate.Day);
					break;

				case RecurrencyFrequency.Yearly:
					startRecurrencyDate = new DateTime(start.Year, eventDate.Month, eventDate.Day);
					break;

				default:
					new ArgumentException("The specified recurrency is not valid.").Throw();
					return null;
			}

			while (startRecurrencyDate <= start)
			{
				startRecurrencyDate = NextDate(startRecurrencyDate.Value, recurrency);
			}

			return startRecurrencyDate.Value.Date.Add(eventDate.TimeOfDay);
		}

		public static CalendarRange Range(
			DateTime date,
			CalendarView vista,
			DayOfWeek firstDayOfWeek = DayOfWeek.Monday,
			DayOfWeek lastDayOfWeek = DayOfWeek.Sunday)
		{
			var selectedDate = date.Date;

			var firstDay = selectedDate.FirstDayOfWeek();

			if (vista == CalendarView.Day)
			{
				firstDay = date;
			}

			if (vista != CalendarView.Month)
			{
				var dates =
					Enumerable
						.Range(0, (int)vista)
						.Select(dayCount => firstDay.AddDays(dayCount))
						.ToList();

				return new CalendarRange(dates.First(), dates.Last());
			}

			var firstDayOfMonth = date.FirstDayOfMonth();
			var first = firstDayOfMonth.FirstDayOfWeek(firstDayOfWeek);

			var lastDayOfMonth = date.LastDayOfMonth();
			var last = lastDayOfMonth.LastDayOfWeek(lastDayOfWeek);

			return new CalendarRange(first, last);
		}
	}
}