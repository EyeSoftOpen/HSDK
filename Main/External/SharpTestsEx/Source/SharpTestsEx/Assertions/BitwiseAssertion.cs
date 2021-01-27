using System;
using System.Text;

namespace SharpTestsEx.Assertions
{
	public abstract class BitwiseAssertion<TA> : UnaryAssertion<TA>
	{
		private readonly string @operator;
		private readonly UnaryAssertion<TA> left;
		private readonly UnaryAssertion<TA> right;

		protected BitwiseAssertion(string @operator, UnaryAssertion<TA> left, UnaryAssertion<TA> right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (right == null)
			{
				throw new ArgumentNullException("right");
			}
			this.left = left;
			this.right = right;
			this.@operator = @operator;
		}

		public UnaryAssertion<TA> Left => left;

        public UnaryAssertion<TA> Right => right;

        public string Operator => @operator;

        public override string GetMessage(TA actual, string customMessage)
		{
			var sb = new StringBuilder(500);
			sb.AppendLine(GetUnaryFailureMessage(left, actual));
			sb.AppendLine(@operator);
			if (string.IsNullOrEmpty(customMessage))
			{
				sb.Append(GetUnaryFailureMessage(right, actual));
			}
			else
				if (!string.IsNullOrEmpty(customMessage))
				{
					sb.AppendLine(GetUnaryFailureMessage(right, actual));
					sb.Append(customMessage);
				}
			return sb.ToString();
		}

		protected string GetUnaryFailureMessage(UnaryAssertion<TA> unaryAssertion, TA actual)
		{
			return string.Format("{0}", unaryAssertion.GetMessage(actual, string.Empty));
		}
	}
}