namespace EyeSoft.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Holidays
    {
        private readonly HolidayEntries holidayEntries = new HolidayEntries();

        private readonly int startingYear;
        private readonly int futureYears;

        internal Holidays(int startingYear, int futureYears)
        {
            this.startingYear = startingYear;
            this.futureYears = futureYears + 1;
        }

        public Holidays Fixed(string name, AgnosticDay agnosticDay)
        {
            holidayEntries.Check(agnosticDay);

            var holidays =
                Enumerable
                    .Range(startingYear, futureYears)
                    .Select(currentYear => FixedHoliday(name, agnosticDay, currentYear))
                    .ToList();

            holidayEntries.AddRange(holidays, true);

            return this;
        }

        public Holidays Entry(string name, params AgnosticDay[] dates)
        {
            var holidays =
                dates
                    .Select(
                        (agnosticDay, year) =>
                            Holiday.Entry(name, new DateTime(startingYear + year, agnosticDay.Month, agnosticDay.Day)))
                    .ToList();

            var orderedHolidays = holidays.OrderBy(holiday => holiday.Date);

            holidayEntries.AddRange(holidays, false);

            return this;
        }

        public Holidays NextDay(string description)
        {
            var nextDayHolidays =
                holidayEntries
                    .LastHolidaysAdded
                    .Select(holiday => Holiday.Create(description, holiday.Date.AddDays(1), holidayEntries.LastWasFixed));

            holidayEntries.AddRange(nextDayHolidays.ToList());

            return this;
        }

        public IEnumerable<Holiday> List()
        {
            return
                holidayEntries.List();
        }

        private Holiday FixedHoliday(string descrizione, AgnosticDay agnosticDay, int currentYear)
        {
            var dateTime = new DateTime(currentYear, agnosticDay.Month, agnosticDay.Day);

            return
                Holiday.Fixed(descrizione, dateTime);
        }
    }
}