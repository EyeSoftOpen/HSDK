using System;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.ExtensionsImpl
{
	public class StringConstraints : NegableConstraints<string, IStringConstraints>, IStringConstraints
	{
		public StringConstraints(string actual) : base(actual) { }
		public StringConstraints(string actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }

		public IStringBeConstraints Be => new StringBeConstraints(this);

        public object Clone()
		{
			return new StringConstraints(AssertionInfo.Actual, () => AssertionInfo.FailureMessage);
		}
	}

	public class StringBeConstraints : ChildAndChainableConstraints<string, IStringConstraints>, IStringBeConstraints
	{
		public static readonly StringEqualityMessageBuilder DefaultEqualityMessageBuilder = new StringEqualityMessageBuilder();
		public StringBeConstraints(IStringConstraints parent) : base(parent) { }

		#region Implementation of IStringConstraints

		public IAndConstraints<IStringConstraints> EqualTo(string expected)
		{
			AssertionInfo.AssertUsing(new Assertion<string, string>(Properties.Resources.PredicateBeEqualTo, expected, a => string.Equals(a, expected), DefaultEqualityMessageBuilder));
			return AndChain;
		}

		/// <summary>
		/// Verifies that the <see cref="string"/> is null.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		public IAndConstraints<IStringConstraints> Null()
		{
			AssertionInfo.AssertUsing(new NullAssertion<string>());
			return AndChain;
		}

		/// <summary>
		/// Verifies that the <see cref="string"/> is empty.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		public IAndConstraints<IStringConstraints> Empty()
		{
			AssertionInfo.AssertUsing(new Assertion<string, string>(Properties.Resources.PredicateBeEmpty, string.Empty, a => string.Empty.Equals(a)));
			return AndChain;
		}

		#endregion
	}
}