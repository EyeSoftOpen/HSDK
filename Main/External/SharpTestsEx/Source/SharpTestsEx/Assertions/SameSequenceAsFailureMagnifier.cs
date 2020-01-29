using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTestsEx.Assertions
{
	public class SameSequenceAsFailureMagnifier<T>: IFailureMagnifier
	{
		private readonly IEnumerable<T> actual;
		private readonly IEnumerable<T> expected;
		private readonly IEqualityComparer<T> comparer;

		public SameSequenceAsFailureMagnifier(IEnumerable<T> actual, IEnumerable<T> expected)
			: this(actual, expected, EqualityComparer<T>.Default)
		{
		}

		public SameSequenceAsFailureMagnifier(IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> comparer)
		{
			this.actual = actual;
			this.expected = expected;
			this.comparer = comparer;
		}

		#region Implementation of IFailureMagnifier

		public string Message()
		{
			if (ReferenceEquals(null, actual) || ReferenceEquals(null, expected))
			{
				return string.Empty;
			}

			int firstDifference = actual.PositionOfFirstDifference(expected, comparer);
			if (firstDifference < 0)
			{
				return string.Empty;
			}

			var sb = new StringBuilder(100);
			sb.AppendLine(string.Format(Properties.Resources.FailureMsgEnumerableDiffPosTmpl,
																	firstDifference == 0 ? firstDifference.ToString() : firstDifference + " (zero based)"));
			sb.AppendLine(string.Format(Properties.Resources.ExpectedTmpl,
																	Messages.FormatValue(expected.ElementAtOrDefault(firstDifference))));
			sb.AppendLine(string.Format(Properties.Resources.FoundTmpl,
																	Messages.FormatValue(actual.ElementAtOrDefault(firstDifference))));
			return sb.ToString();
		}

		#endregion
	}
}