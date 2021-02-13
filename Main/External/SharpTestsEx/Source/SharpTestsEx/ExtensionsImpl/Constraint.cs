using System;

namespace SharpTestsEx.ExtensionsImpl
{
	public class Constraint<T> : IConstraints<T>
	{
		protected Constraint(T actual) : this(new AssertionInfo<T>(actual)) { }

		protected Constraint(T actual, Func<string> messageBuilder) : this(new AssertionInfo<T>(actual, messageBuilder)) { }

		private Constraint(IAssertionInfo<T> assertionInfo)
		{
			AssertionInfo = assertionInfo;
		}

		#region Implementation of IConstraints<T>

		public IAssertionInfo<T> AssertionInfo { get; }

		public IConstraints<T> Parent => null;

        #endregion
	}
}