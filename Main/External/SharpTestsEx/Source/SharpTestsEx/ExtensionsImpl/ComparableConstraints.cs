using System;
using SharpTestsEx.Assertions;
using SharpTestsEx.Properties;

namespace SharpTestsEx.ExtensionsImpl
{
#pragma warning disable 1591
	public class ComparableConstraints<T> : NegableConstraints<T, IComparableConstraints<T>>, IComparableConstraints<T>
	{
		public ComparableConstraints(T actual) : base(actual) {}
		public ComparableConstraints(T actual, Func<string> messageBuilder) : base(actual, messageBuilder) {}

		public IComparableBeConstraints<T> Be
		{
			get { return new ComparableBeConstraints<T>(this); }
		}

		public object Clone()
		{
			return new ComparableConstraints<T>(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}
	}

	public class ComparableBeConstraints<T> : ChildAndChainableConstraints<T, IComparableConstraints<T>>,
	                                          IComparableBeConstraints<T>
	{
		public ComparableBeConstraints(IComparableConstraints<T> parent) : base(parent) {}

		#region Implementation of IComparableConstraints<T>

		public IAndConstraints<IComparableConstraints<T>> EqualTo(IComparable expected)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Properties.Resources.PredicateBeEqualTo, expected,
			                                                        a => NumericsComparer.Default.Compare(a, expected) == 0));
			return AndChain;
		}

		public IAndConstraints<IComparableConstraints<T>> GreaterThan(IComparable expected)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Properties.Resources.PredicateBeGreaterThan, expected,
			                                                        a => NumericsComparer.Default.Compare(a, expected) > 0));
			return AndChain;
		}

		public IAndConstraints<IComparableConstraints<T>> LessThan(IComparable expected)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Properties.Resources.PredicateBeLessThan, expected,
			                                                        a => NumericsComparer.Default.Compare(a, expected) < 0));
			return AndChain;
		}

		public IAndConstraints<IComparableConstraints<T>> GreaterThanOrEqualTo(IComparable expected)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Properties.Resources.PredicateBeGreaterThanOrEquaTo, expected,
			                                                        a => NumericsComparer.Default.Compare(a, expected) >= 0));
			return AndChain;
		}

		public IAndConstraints<IComparableConstraints<T>> LessThanOrEqualTo(IComparable expected)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Properties.Resources.PredicateBeLessThanOrEqualTo, expected,
			                                                        a => NumericsComparer.Default.Compare(a, expected) <= 0));
			return AndChain;
		}

		public IAndConstraints<IComparableConstraints<T>> IncludedIn(IComparable lowLimit, IComparable highLimit)
		{
			AssertionInfo.AssertUsing(new Assertion<T, IComparable>(Resources.PredicateBeInRange, null,
			                                                        a =>
			                                                        NumericsComparer.Default.Compare(a, lowLimit) >= 0
			                                                        && NumericsComparer.Default.Compare(a, highLimit) <= 0,
			                                                        mi =>
			                                                        string.Format("{0} {1} {2} [{3}:{4}].{5}",
			                                                                      Messages.FormatValue(mi.Actual),
			                                                                      Resources.AssertionVerb,
			                                                                      mi.AssertionPredicate,
			                                                                      lowLimit, highLimit,
			                                                                      mi.CustomMessage)));
			return AndChain;
		}

		#endregion
	}
#pragma warning restore 1591
}