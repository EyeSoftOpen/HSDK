using System;
using System.Collections.Generic;
using System.Text;
using SharpTestsEx.Properties;

namespace SharpTestsEx.Assertions
{
	public class EnumerableFailureMagnifier<T> : IFailureMagnifier
	{
		private readonly IEnumerable<T> actual;
		private readonly Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> differencesDelegate;
		private readonly IEnumerable<T> expected;

		public EnumerableFailureMagnifier(IEnumerable<T> actual, IEnumerable<T> expected,
		                                  Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> differencesDelegate)
		{
			this.actual = actual;
			this.expected = expected;
			this.differencesDelegate = differencesDelegate;
		}

		#region Implementation of IFailureMagnifier

		public string Message()
		{
			if (differencesDelegate == null || ReferenceEquals(null, actual) || ReferenceEquals(null, expected))
			{
				return string.Empty;
			}
			var result = new StringBuilder(200);
			result.AppendLine(Resources.FailureMsgDifferences + Messages.FormatEnumerable(differencesDelegate(actual, expected)));
			return result.ToString();
		}

		#endregion
	}
}