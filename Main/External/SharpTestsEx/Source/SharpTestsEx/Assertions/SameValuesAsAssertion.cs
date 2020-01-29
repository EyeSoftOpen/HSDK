using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.Assertions
{
	public class SameValuesAsAssertion<T> : Assertion<IEnumerable<T>, IEnumerable<T>>
	{
		public SameValuesAsAssertion(IEnumerable<T> expected)
			: base(Properties.Resources.PredicateHaveSameValuesAs, expected, a =>
				{
					if (ReferenceEquals(a, expected))
					{
						return true;
					}
					if (a == null || expected == null)
					{
						return false;
					}
					var enumeratedActual = a.ToArray();
					var enumeratedExpected = expected.ToArray();
					return !enumeratedActual.Except(enumeratedExpected).Concat(enumeratedExpected.Except(enumeratedActual)).Any();
				}, mi => string.Empty) {}

		protected override Func<MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>>, string> MessageBuilder
		{
			get
			{
				var mb = new EnumerableMessageBuilder<T>((a, e) => a.Except(e).Concat(Expected.Except(a)));
				return mb.Compose; 
			}
		}
	}
}