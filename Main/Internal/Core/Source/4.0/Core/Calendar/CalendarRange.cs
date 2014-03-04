namespace EyeSoft.Calendar
{
	using System;

	using EyeSoft.Extensions;

	public class CalendarRange
	{
		public CalendarRange(DateTime first, DateTime last)
		{
			First = first;
			Last = last;

			Days = Last.Subtract(First).Days;
		}

		public DateTime First { get; private set; }

		public DateTime Last { get; private set; }

		public int Days { get; private set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (CalendarRange)obj;
			return First.Equals(other.First) && Last.Equals(other.Last);
		}

		public override int GetHashCode()
		{
			return ObjectHash.Combine(First, Last);
		}
	}
}