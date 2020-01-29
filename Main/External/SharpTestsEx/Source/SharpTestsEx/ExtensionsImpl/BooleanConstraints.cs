using System;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
	public class BooleanConstraints : IBooleanConstraints
	{
		private readonly IBooleanBeConstraints concreteConstraint;

		public BooleanConstraints(IBooleanBeConstraints concreteConstraint)
		{
			this.concreteConstraint = concreteConstraint;
		}

		public IBooleanBeConstraints Be
		{
			get { return concreteConstraint; }
		}
	}

	public class BooleanBeConstraints : Constraint<bool>, IBooleanBeConstraints
	{
		public BooleanBeConstraints(bool actual) : base(actual) {}
		public BooleanBeConstraints(bool actual, Func<string> messageBuilder) : base(actual, messageBuilder) {}

		#region Implementation of IBoleanConstraints

		public void True()
		{
			AssertionInfo.AssertUsing(new Assertion<bool, bool>(Properties.Resources.Be, true, a => a));
		}

		public void False()
		{
			AssertionInfo.AssertUsing(new Assertion<bool, bool>(Properties.Resources.Be, false, a => !a));
		}

		public void EqualTo(bool expected)
		{
			AssertionInfo.AssertUsing(new Assertion<bool, bool>(Properties.Resources.Be, expected, a => a == expected));
		}

		#endregion
	}
}