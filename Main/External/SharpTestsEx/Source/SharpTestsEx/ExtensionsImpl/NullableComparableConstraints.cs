using System;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
	public class NullableComparableConstraints<T> : NegableConstraints<T, INullableComparableConstraints<T>>,
		INullableComparableConstraints<T>, IComparableConstraints<T>
	{
		public NullableComparableConstraints(T actual) : base(actual) { }
		public NullableComparableConstraints(T actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		#region Implementation of IBeConstraints<IComparableBeConstraints<T>>

		public IComparableBeConstraints<T> Be => new ComparableBeConstraints<T>(this);

        #endregion

		#region Implementation of IAllowClone

		public object Clone()
		{
			return new NullableComparableConstraints<T>(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}

		#endregion

		#region Implementation of IHaveConstraints<IHaveNullableComparableConstraints<T>>

		public IHaveNullableComparableConstraints<T> Have => new HaveNullableComparableConstraints<T>(this);

        #endregion

		#region INegableConstraints<IComparableConstraints<T>> Members

		IComparableConstraints<T> INegableConstraints<IComparableConstraints<T>>.Not => Not as IComparableConstraints<T>;

        #endregion
	}

	public class HaveNullableComparableConstraints<T> : ChildAndChainableConstraints<T, INullableComparableConstraints<T>>, IHaveNullableComparableConstraints<T>
	{
		public HaveNullableComparableConstraints(INullableComparableConstraints<T> parent) : base(parent) { }

		#region Implementation of IHaveNullableComparableConstraints<T>

		public void Value()
		{
			AssertionInfo.AssertUsing(new Assertion<T, object>(Properties.Resources.PredicateHaveValue, null, a => a != null));
		}

		#endregion
	}
}