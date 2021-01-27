using System;

namespace SharpTestsEx.ExtensionsImpl
{
	public interface INegable
	{
		void Nagate();
	}

	public class AssertionInfo<T> : IAssertionInfo<T>, INegable
	{
		private readonly Func<string> messageBuilder;
		public AssertionInfo(T actual)
		{
			Actual = actual;
		}

		public AssertionInfo(T actual, Func<string> messageBuilder)
		{
			this.messageBuilder = messageBuilder;
			Actual = actual;
		}

		#region Implementation of IAssertionInfo<T>

		public T Actual { get; private set; }

		public bool IsNegated { get; private set; }

		public string FailureMessage => messageBuilder != null ? messageBuilder() : null;

        #endregion

		#region INegable Members

		void INegable.Nagate()
		{
			IsNegated = !IsNegated;
		}

		#endregion
	}
}