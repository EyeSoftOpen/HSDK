using System;

namespace SharpTestsEx.Assertions
{
	public class StringEqualityMessageBuilder : IMessageBuilder<string, string>
	{
		const int FlatValueMaxAcceptableLength = 80;
		const string Ellipsis = "...";

		#region Implementation of IMessageBuilder

		public string Compose(MessageBuilderInfo<string, string> info)
		{
			var a = info.Actual;
			var e = info.Expected;
			var baseMessage = string.Format("{0} {1} {2} {3}.{4}", FormatValue(a), Properties.Resources.AssertionVerb,
																			info.AssertionPredicate, FormatValue(e), info.CustomMessage);

			var m = new StringEqualityFailureMagnifier(()=> a, ()=> e);
			var magnMessage = m.Message();
			return string.IsNullOrEmpty(magnMessage) ? baseMessage : string.Concat(baseMessage, Environment.NewLine, magnMessage);
		}

		#endregion

		public static string FormatValue(string value)
		{
			if (ReferenceEquals(null, value))
			{
				return Properties.Resources.NullValue;
			}
			return string.Format("\"{0}\"",
													 value.Length <= FlatValueMaxAcceptableLength
														? value
														: value.Substring(0, FlatValueMaxAcceptableLength) + Ellipsis);
		}
	}
}