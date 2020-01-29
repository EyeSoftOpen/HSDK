using System;
using System.Text;
using SharpTestsEx.Factory;

namespace SharpTestsEx.Assertions
{
	public class AndAssertion<TA> : BitwiseAssertion<TA>
	{
		public AndAssertion(UnaryAssertion<TA> left, UnaryAssertion<TA> right) : base("And", left, right) {}

		public override Func<TA, bool> Matcher
		{
			get { return a => Left.Matcher(a) && Right.Matcher(a); }
		}

		public override void Assert(TA actual, string customMessage)
		{
			// To prevent double execution of the matcher the this method does not use base.Assert nor base.GetMessage(TA,string)
			var leftMatch = Left.Matcher(actual);
			var rigthMatch = Right.Matcher(actual);
			if(leftMatch && rigthMatch)
			{
				return;
			}
			throw AssertExceptionFactory.CreateException(GetMessage(leftMatch, rigthMatch, actual, customMessage));
		}

		private string GetMessage(bool leftMatch, bool rigthMatch, TA actual, string customMessage)
		{
			var sb = new StringBuilder(500);
			
			if (!leftMatch)
			{
				sb.AppendLine(GetUnaryFailureMessage(Left, actual));
			}

			if (!leftMatch && !rigthMatch)
			{
				sb.AppendLine(Operator);
			}

			var hasCustomMessage = string.IsNullOrEmpty(customMessage);
			if (!rigthMatch)
			{
				if (hasCustomMessage)
				{
					sb.Append(GetUnaryFailureMessage(Right, actual));
				}
				else
				{
					sb.AppendLine(string.Format("{0}", Right.GetMessage(actual, string.Empty)));
				}
			}
			if (hasCustomMessage)
			{
				sb.Append(customMessage);
			}
			return sb.ToString();
		}

		public override string GetMessage(TA actual, string customMessage)
		{
			return GetMessage(Left.Matcher(actual), Right.Matcher(actual), actual, customMessage);
		}
	}
}