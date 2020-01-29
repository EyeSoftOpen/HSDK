using System;
using System.Linq.Expressions;

namespace SharpTestsEx.Assertions
{
	public class ExpressionAssertion<TA> : UnaryAssertion<TA>
	{
		private readonly Func<TA, bool> compiledMatcher;

		public ExpressionAssertion(Expression<Func<TA, bool>> expression)
			: this(expression, new ExpressionMessageComposer<TA>(expression)) {}

		public ExpressionAssertion(Expression<Func<TA, bool>> expression, IMessageComposer<TA> messageComposer)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (messageComposer == null)
			{
				throw new ArgumentNullException("messageComposer");
			}
			MessageComposer = messageComposer;
			compiledMatcher = expression.Compile();
		}

		public IMessageComposer<TA> MessageComposer { get; set; }

		public override Func<TA, bool> Matcher
		{
			get { return compiledMatcher; }
		}

		public override string GetMessage(TA actual, string customMessage)
		{
			return MessageComposer.GetMessage(actual, customMessage);
		}
	}
}