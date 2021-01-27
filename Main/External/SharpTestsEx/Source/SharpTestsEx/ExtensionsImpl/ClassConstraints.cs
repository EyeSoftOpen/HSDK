using System;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
#pragma warning disable 1591
	public class ClassConstraints : NegableConstraints<object, IClassConstraints>, IClassConstraints
	{
		public ClassConstraints(object actual) : base(actual) { }
		public ClassConstraints(object actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		#region IClassConstraints Members

		public IClassBeConstraints Be => new ClassBeConstraints(this);

        #endregion

		public object Clone()
		{
			return new ClassConstraints(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}
	}

	public class ClassConstraints<TValue> : ClassConstraints, IClassConstraints<TValue>
	{
		public ClassConstraints(object actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		public TValue ValueOf => (TValue)AssertionInfo.Actual;

        public TValue Value => (TValue)AssertionInfo.Actual;
    }

	public class ClassBeConstraints : ChildAndChainableConstraints<object, IClassConstraints>, IClassBeConstraints
	{
		public ClassBeConstraints(IClassConstraints parent) : base(parent) { }

		#region Implementation of IClassConstraints

		public IAndConstraints<IClassConstraints> EqualTo(object expected)
		{
			AssertionInfo.AssertUsing(new ObjectEqualsAssertion<object, object>(expected));
			return AndChain;
		}

		public IAndConstraints<IClassConstraints> Null()
		{
			AssertionInfo.AssertUsing(new NullAssertion<object>());
			return AndChain;
		}

		public IAndConstraints<IClassConstraints<T>> OfType<T>()
		{
			AssertionInfo.AssertUsing(new TypeOfAssertion(typeof(T)));
			return
				new AndConstraint<IClassConstraints<T>>(new ClassConstraints<T>((T)AssertionInfo.Actual,
																																				() => AssertionInfo.FailureMessage));
		}

		#endregion
	}
#pragma warning restore 1591
}