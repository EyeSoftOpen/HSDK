using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.Assertions
{
	public class SameSequenceAsAssertion<T> : Assertion<IEnumerable<T>, IEnumerable<T>>
	{
		public SameSequenceAsAssertion(IEnumerable<T> expected)
			: base(Properties.Resources.PredicateHaveSameSequenceAs, expected, a =>
				{
					if (ReferenceEquals(a, expected))
					{
						return true;
					}
					if (a == null || expected == null)
					{
						return false;
					}
					return a.SequenceEqual(expected);
				}, new SameSequenceAsMessageBuilder<T>()) { }
	}
}