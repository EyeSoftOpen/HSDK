using System;

namespace SharpTestsEx.Assertions
{
	public class NegateAssertion<TA> : UnaryAssertion<TA>
	{
		private readonly UnaryAssertion<TA> source;

		public NegateAssertion(UnaryAssertion<TA> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
			source.Predicate = Properties.Resources.Negation + " " + source.Predicate;
		}

		public override string Predicate => source.Predicate;

        public override Func<TA, bool> Matcher
		{
			get
			{
				return a => !source.Matcher(a);
			}
		}

		public override string GetMessage(TA actual, string customMessage)
		{
			return source.GetMessage(actual, customMessage);
		}
	}
}