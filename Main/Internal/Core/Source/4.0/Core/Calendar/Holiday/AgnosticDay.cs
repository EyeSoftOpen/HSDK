namespace EyeSoft.Calendar
{
    using System;

    public class AgnosticDay : IComparable<AgnosticDay>
    {
        public AgnosticDay(int day, int month)
        {
            Day = day;
            Month = month;
        }

        public int Day { get; private set; }

        public int Month { get; private set; }

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