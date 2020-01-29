using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.Assertions
{
	public class SubsetOfAssertion<T> : Assertion<IEnumerable<T>, IEnumerable<T>>
	{
		public SubsetOfAssertion(IEnumerable<T> expected) : base(Properties.Resources.PredicateBeSubsetOf, expected, a =>
			{
				if (ReferenceEquals(a, expected))
				{
					return true;
				}
				if (a == null || expected == null)
				{
					return false;
				}
				IEnumerable<T> intersect = a.Intersect(expected);
				return a.Count() == intersect.Count();
			}, mi => string.Empty) {}

		protected override Func<MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>>, string> MessageBuilder
		{
			get
			{
				var mb = new EnumerableMessageBuilder<T>((a, e) => a.Except(e));
				return mb.Compose;
			}
		}
	}
}