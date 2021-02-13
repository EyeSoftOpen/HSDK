namespace EyeSoft.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class ArrayExtensions
	{
		public static T[] TypedClone<T>(this T[] array)
		{
			return (T[])array.Clone();
		}

		public static T[] Shuffle<T>(this T[] array)
		{
			var random = new Random();

			var clone = array.TypedClone();

			for (var i = array.Length; i > 1; i--)
			{
				var j = random.Next(i);

				Swap(clone, j, i);
			}

			return clone;
		}

		private static void Swap<T>(IList<T> clone, int j, int i)
		{
			var tmp = clone[j];
			clone[j] = clone[i - 1];
			clone[i - 1] = tmp;
		}
	}
}
