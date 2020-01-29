using System;
using System.Linq.Expressions;
using SharpTestsEx.Properties;

namespace SharpTestsEx.Assertions
{
	public class ExpressionMessageComposer<TA> : IMessageComposer<TA>
	{
		private static readonly IFailureMagnifier Empty = new EmptyMagnifier();
		private readonly Expression<Func<TA, bool>> expression;
		private readonly IFailureMagnifier magnifier;

		public ExpressionMessageComposer(Expression<Func<TA, bool>> expression) : this(expression, Empty) {}

		public ExpressionMessageComposer(Expression<Func<TA, bool>> expression, IFailureMagnifier magnifier)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (magnifier == null)
			{
				throw new ArgumentNullException("magnifier");
			}
			this.expression = expression;
			this.magnifier = magnifier;
		}

		#region Implementation of IMessageComposer<TA>

		public string GetMessage(TA actual, string customMessage)
		{
			string baseMessage;
			if (!string.IsNullOrEmpty(customMessage))
			{
				baseMessage = string.Format("{3}: {0} {1} {2}", Messages.FormatValue(actual), Resources.AssertionVerb,
				                            string.Format("Satisfy ({0})", new ExpressionStringBuilder(expression)), customMessage);
			}
			else
			{
				baseMessage = string.Format("{0} {1} {2}", Messages.FormatValue(actual), Resources.AssertionVerb,
				                            string.Format("Satisfy ({0})", new ExpressionStringBuilder(expression)));
			}
			string magnMessage = magnifier.Message();
			return string.IsNullOrEmpty(magnMessage) ? baseMessage : string.Concat(baseMessage, Environment.NewLine, magnMessage);
		}

		#endregion
	}
}