namespace EyeSoft.Calendar
{
    using System;

    public class AgnosticDay : IComparable<AgnosticDay>
    {
        public AgnosticDay(int day, int month)
        {
            if (day < 1 || day > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            Day = day;
            Month = month;
        }

        public int Day { get; }

        public int Month { get; }

        public int CompareTo(AgnosticDay other)
        {
            if (Month > other.Month)
            {
                return 1;
            }

            if (Month < other.Month)
            {
                return -1;
            }

            return Day.CompareTo(other.Day);
        }

        public override string ToString()
        {
            return $"Day: {Day} Month: {Month}";
        }
    }
}