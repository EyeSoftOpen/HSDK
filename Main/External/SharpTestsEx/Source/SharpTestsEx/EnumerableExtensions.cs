using System;
using System.Collections.Generic;

namespace SharpTestsEx
{
	/// <summary>
	/// <see cref="IEnumerable{T}"/> etensions methods.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Find the first position where two sequence differ
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="first">An <see cref="IEnumerable{T}"/> to compare to second</param>
		/// <param name="second">An <see cref="IEnumerable{T}"/> to compare to the first sequence. </param>
		/// <returns>The position of the first difference; otherwise -1 where the two sequences has the same sequence.</returns>
		public static int PositionOfFirstDifference<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return PositionOfFirstDifference(first, second, EqualityComparer<TSource>.Default);
		}

		public static int PositionOfFirstDifference<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}

			int diffPos = -1;
			using (IEnumerator<TSource> firstEnumerator = first.GetEnumerator())
			{
				using (IEnumerator<TSource> secondEnumerator = second.GetEnumerator())
				{
					while (firstEnumerator.MoveNext())
					{
						diffPos++;
						if (!secondEnumerator.MoveNext() || !comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
						{
							return diffPos;
						}
					}
					if (secondEnumerator.MoveNext())
					{
						return diffPos == -1 ? 0 : ++diffPos;
					}
				}
			}
			return -1;
		}

	}
}