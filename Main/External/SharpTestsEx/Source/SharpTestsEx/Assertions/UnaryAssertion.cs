using System;
using SharpTestsEx.Factory;

namespace SharpTestsEx.Assertions
{
	public class UnaryAssertion<TA> : IAssertion<TA>, IMessageComposer<TA>, IAssertionMatcher<TA>
	{
		protected UnaryAssertion()
		{
			Predicate = string.Empty;
		}

		public UnaryAssertion(string predicate, Func<TA, bool> matcher)
		{
			if (matcher == null)
			{
				throw new ArgumentNullException("matcher");
			}
			Predicate = predicate ?? string.Empty;
			Matcher = matcher;
		}

		public virtual string Predicate { get; set; }

		#region IAssertionMatcher<TA> Members

		public virtual Func<TA, bool> Matcher { get; }

		#endregion

		#region Implementation of IAssertion<TA>

		public virtual void Assert(TA actual, string customMessage)
		{
			if (Matcher(actual))
			{
				return;
			}

			throw AssertExceptionFactory.CreateException(GetMessage(actual, customMessage));
		}

		#endregion

		public static IAssertion<TA> operator !(UnaryAssertion<TA> source)
		{
			return new NegateAssertion<TA>(source);
		}

		public static IAssertion<TA> operator |(UnaryAssertion<TA> x, UnaryAssertion<TA> y)
		{
			return new OrAssertion<TA>(x, y);
		}

		public static IAssertion<TA> operator &(UnaryAssertion<TA> x, UnaryAssertion<TA> y)
		{
			return new AndAssertion<TA>(x, y);
		}

		#region Implementation of IMessageComposer<TA>

		public virtual string GetMessage(TA actual, string customMessage)
		{
			return string.Format("{0} {1} {2}.{3}", Messages.FormatValue(actual), Properties.Resources.AssertionVerb, Predicate, customMessage);
		}

		#endregion
	}
}