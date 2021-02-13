using System;

namespace SharpTestsEx.Assertions
{
	/// <summary>
	/// Represent a Assertion template where the real logic is delegated.
	/// </summary>
	/// <typeparam name="TA">Type of the actual value.</typeparam>
	/// <typeparam name="TE">Type of the expected value.</typeparam>
	public class Assertion<TA, TE> : UnaryAssertion<TA>
	{
		public Assertion(string predicate, TE expected, Func<TA, bool> match,
		                 Func<MessageBuilderInfo<TA, TE>, string> messageBuilder) : base(predicate, match)
		{
			if (messageBuilder == null)
			{
				throw new ArgumentNullException("messageBuilder");
			}
			Expected = expected;
			MessageBuilder = messageBuilder;
		}

		public Assertion(string predicate, TE expected, Func<TA, bool> match, IMessageBuilder<TA, TE> messageBuilder)
			: this(
				predicate, expected, match,
				(messageBuilder != null) ? messageBuilder.Compose : (Func<MessageBuilderInfo<TA, TE>, string>) null) {}

		public Assertion(string predicate, TE expected, Func<TA, bool> match)
			: this(predicate, expected, match, new DefaultMessageBuilder<TA, TE>()) {}

		protected virtual TE Expected { get; }

		protected virtual Func<MessageBuilderInfo<TA, TE>, string> MessageBuilder { get; }

		protected virtual MessageBuilderInfo<TA, TE> GetMessageBuilderInfo(TA actual, string customMessage)
		{
			return new MessageBuilderInfo<TA, TE>
			       	{Actual = actual, Expected = Expected, AssertionPredicate = Predicate, CustomMessage = customMessage};
		}

		public override string GetMessage(TA actual, string customMessage)
		{
			return MessageBuilder(GetMessageBuilderInfo(actual, customMessage));
		}
	}
}