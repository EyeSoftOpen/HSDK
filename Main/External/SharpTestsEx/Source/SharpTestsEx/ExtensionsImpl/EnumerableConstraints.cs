using System;
using System.Collections.Generic;
using System.Linq;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
	public class EnumerableConstraints<T> : NegableConstraints<IEnumerable<T>, IEnumerableConstraints<T>>,
	                                        IEnumerableConstraints<T>
	{
		public EnumerableConstraints(IEnumerable<T> actual) : base(actual) {}
		public EnumerableConstraints(IEnumerable<T> actual, Func<string> messageBuilder) : base(actual, messageBuilder) {}

		public IEnumerableBeConstraints<T> Be
		{
			get { return new EnumerableBeConstraints<T>(this); }
		}

		public IEnumerableHaveConstraints<T> Have
		{
			get { return new EnumerableHaveConstraints<T>(this); }
		}

		public object Clone()
		{
			return new EnumerableConstraints<T>(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}
	}

	public class EnumerableBeConstraints<T> : ChildAndChainableConstraints<IEnumerable<T>, IEnumerableConstraints<T>>,
	                                          IEnumerableBeConstraints<T>
	{
		public EnumerableBeConstraints(IEnumerableConstraints<T> parent) : base(parent) {}

		#region Implementation of IEnumerableConstraints<T>

		/// <summary>
		/// Verifies that the <see cref="IEnumerable{T}"/> instance is null.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		public IAndConstraints<IEnumerableConstraints<T>> Null()
		{
			AssertionInfo.AssertUsing(new Assertion<IEnumerable<T>, IEnumerable<T>>(Properties.Resources.PredicateBeNull, null,
			                                                                        a => ReferenceEquals(a, null),
			                                                                        new UnaryEnumerableMessageBuilder<T>()));
			return AndChain;
		}

		/// <summary>
		/// Verifies that the <see cref="IEnumerable{T}"/> is empty.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		public IAndConstraints<IEnumerableConstraints<T>> Empty()
		{
			AssertionInfo.AssertUsing(new Assertion<IEnumerable<T>, IEnumerable<T>>(Properties.Resources.PredicateBeEmpty, null,
			                                                                        a => a != null && !a.Any(),
			                                                                        new UnaryEnumerableMessageBuilder<T>()));
			return AndChain;
		}

		#endregion
	}

	public class EnumerableHaveConstraints<T> : ChildAndChainableConstraints<IEnumerable<T>, IEnumerableConstraints<T>>,
	                                            IEnumerableHaveConstraints<T>
	{
		public EnumerableHaveConstraints(IEnumerableConstraints<T> parent) : base(parent) {}

		public IAndConstraints<IEnumerableConstraints<T>> SameSequenceAs(IEnumerable<T> expected)
		{
			AssertionInfo.AssertUsing(new SameSequenceAsAssertion<T>(expected));
			return AndChain;
		}

		public IComparableBeConstraints<int> Count
		{
			get
			{
				var immediateNotNull = new Assertion<IEnumerable<T>, IEnumerable<T>>(Properties.Resources.Negation +" "+ Properties.Resources.PredicateBeNull, null,
																																				a => !ReferenceEquals(a, null),
																																				new UnaryEnumerableMessageBuilder<T>());
				immediateNotNull.Assert(AssertionInfo.Actual, AssertionInfo.FailureMessage);

				int count = AssertionInfo.Actual.Count();
				var cc = new ComparableConstraints<int>(count);
				if (AssertionInfo.IsNegated)
				{
					((INegable) cc.AssertionInfo).Nagate();
				}
				return new ComparableBeConstraints<int>(cc);
			}
		}
	}
}