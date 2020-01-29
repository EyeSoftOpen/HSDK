using System;
using System.Collections.Generic;
using System.Text;
using SharpTestsEx.Properties;

namespace SharpTestsEx.Assertions
{
	public class UnaryEnumerableFailureMagnifier<T>: IFailureMagnifier
	{
		private readonly IEnumerable<T> actual;
		private readonly Func<IEnumerable<T>, IEnumerable<T>> differencesDelegate;

		public UnaryEnumerableFailureMagnifier(IEnumerable<T> actual, Func<IEnumerable<T>, IEnumerable<T>> differencesDelegate)
		{
			this.actual = actual;
			this.differencesDelegate = differencesDelegate;
		}

		#region Implementation of IFailureMagnifier

		public string Message()
		{
			if (differencesDelegate == null || ReferenceEquals(null, actual))
			{
				return string.Empty;
			}
			var result = new StringBuilder(200);
			result.AppendLine(Resources.FailureMsgDifferences + Messages.FormatEnumerable(differencesDelegate(actual)));
			return result.ToString();
		}

		#endregion
	}
}