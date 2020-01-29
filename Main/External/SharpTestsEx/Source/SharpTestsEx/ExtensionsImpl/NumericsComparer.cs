using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.ExtensionsImpl
{
	public class NumericsComparer : IComparer
	{
		private class ConvPair
		{
			public ConvPair(Type[] types, Func<object, object, int> comparer)
			{
				Types = types;
				Comparer = comparer;
			}

			public Type[] Types { get; set; }
			public Func<object, object, int> Comparer { get; private set; }

			public bool Match(Type xType, Type yType)
			{
				return Types.FirstOrDefault(x => x == xType || x == yType) != null;
			}
		}

		private static readonly List<ConvPair> comparers;

		static NumericsComparer()
		{
			comparers = new List<ConvPair>
			            	{
			            		new ConvPair(new[] {typeof (double), typeof (float)},
			            		             (x, y) => Convert.ToDouble(x).CompareTo(Convert.ToDouble(y))),
			            		new ConvPair(new[] {typeof (decimal)}, (x, y) => Convert.ToDecimal(x).CompareTo(Convert.ToDecimal(y))),
			            		new ConvPair(new[] {typeof (int), typeof (long), typeof (short), typeof (ushort)},
			            		             (x, y) => Convert.ToInt64(x).CompareTo(Convert.ToInt64(y))),
			            		new ConvPair(new[] {typeof (uint), typeof (ulong)},
			            		             (x, y) => Convert.ToUInt64(x).CompareTo(Convert.ToUInt64(y)))
			            	};
		}

		#region Implementation of IComparer

		public int Compare(object x, object y)
		{
			if (ReferenceEquals(null, x) && ReferenceEquals(null, y))
			{
				return 0;
			}
			if (ReferenceEquals(null, x))
			{
				return -1;
			}
			if (ReferenceEquals(null, y))
			{
				return 1;
			}
			Type xType = x.GetType();
			Type yType = y.GetType();
			if (xType == yType)
			{
				var cx = x as IComparable;
				if (cx != null)
				{
					return cx.CompareTo(y);
				}
			}
			try
			{
				ConvPair convP = comparers.FirstOrDefault(c => c.Match(xType, yType));
				if (convP != null)
				{
					return convP.Comparer(x, y);
				}
			}
			catch (InvalidCastException e)
			{
				throw new ArgumentException("Values are not comparables", e);
			}
			throw new ArgumentException("Values are not comparables");
		}

		#endregion

		public static readonly IComparer Default = new NumericsComparer();
	}
}