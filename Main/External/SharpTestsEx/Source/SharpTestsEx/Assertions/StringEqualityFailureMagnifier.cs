using System;
using System.Text;

namespace SharpTestsEx.Assertions
{
	public class StringEqualityFailureMagnifier: IFailureMagnifier
	{
		const int MagnifierMaxAcceptableLength = 40;
		const int MagnifierLength = 40;
		const string Ellipsis = "...";

		private readonly Func<string> actualGetter;
		private readonly Func<string> expectedGetter;

		public StringEqualityFailureMagnifier(Func<string> actualGetter, Func<string> expectedGetter)
		{
			this.actualGetter = actualGetter;
			this.expectedGetter = expectedGetter;
		}

		#region Implementation of IFailureMagnifier

		public string Message()
		{
			var actual = actualGetter();
			var expected = expectedGetter();
			if (ReferenceEquals(null, actual) || ReferenceEquals(null, expected))
			{
				return string.Empty;
			}

			var firstDifference = actual.PositionOfFirstDifference(expected);
			if (firstDifference < 0)
			{
				return string.Empty;
			}
			var sb = new StringBuilder(200);
			sb.AppendLine(string.Format(Properties.Resources.FailureMsgStringDiffPosTmpl, firstDifference + 1));
			sb.AppendLine(GetStringToShow(actual, firstDifference));
			sb.AppendLine(GetStringToShow(expected, firstDifference));

			var magnifier = (new string('_', Math.Max(actual.Length, expected.Length))).ToCharArray();
			magnifier[firstDifference] = '^';

			sb.AppendLine(GetStringToShow(new string(magnifier), firstDifference));
			return sb.ToString();
		}

		#endregion

		public string GetStringToShow(string original, int magnifierPos)
		{
			const int stringMiddle = (MagnifierLength / 2);

			if (original.Length <= MagnifierMaxAcceptableLength)
			{
				return original;
			}

			var start = (magnifierPos - stringMiddle) < 0
										? 0
										: (magnifierPos + stringMiddle) > original.Length
												? original.Length - MagnifierLength
												: magnifierPos - stringMiddle;

			var sb = new StringBuilder(MagnifierMaxAcceptableLength);
			if (start > 0)
			{
				sb.Append(Ellipsis);
			}
			sb.Append(original.Substring(start, MagnifierLength));
			if ((start + MagnifierLength) < original.Length)
			{
				sb.Append(Ellipsis);
			}
			return sb.ToString();
		}
	}
}