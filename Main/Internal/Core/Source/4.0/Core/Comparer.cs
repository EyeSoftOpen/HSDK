namespace EyeSoft.Core
{
    using System.Collections.Generic;

    public static class Comparer
	{
		public static bool Equals<T>(T first, T second)
		{
			return EqualityComparer<T>.Default.Equals(first, second);
		}

		public static T Min<T>(T first, T second)
		{
			return Compare(first, second) > 0 ? second : first;
		}

		public static T Max<T>(T first, T second)
		{
			return Compare(first, second) > 0 ? first : second;
		}

		public static int Compare<T>(T first, T second)
		{
			return Comparer<T>.Default.Compare(first, second);
		}
	}
}