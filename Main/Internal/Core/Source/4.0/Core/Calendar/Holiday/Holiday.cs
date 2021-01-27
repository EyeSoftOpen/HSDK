namespace EyeSoft.Core.Calendar
{
    using System;
    using Extensions;

    public class Holiday
    {
        private Holiday(string name, DateTime date, bool isFixed)
        {
            Name = name;
            Date = date;
            IsFixed = isFixed;
        }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsFixed { get; private set; }

        public static Holiday Fixed(string name, DateTime dateTime)
        {
            return
                Create(name, dateTime, true);
        }

        public static Holiday Entry(string name, DateTime dateTime)
        {
            return
                Create(name, dateTime, false);
        }

        public static Holiday Create(string name, DateTime dateTime, bool isFixed)
        {
            return
                new Holiday(name, dateTime, isFixed);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj.Convert<Holiday>();

            return
                Name.Equals(other.Name) &&
                Date.Equals(other.Date) &&
                IsFixed.Equals(other.IsFixed);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return "{Name} {Date.ToInvariantCultureDateString()} /fixed: {IsFixed}";
        }
    }
}