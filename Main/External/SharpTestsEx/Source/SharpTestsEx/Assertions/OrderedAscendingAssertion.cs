using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.Assertions
{
	public class OrderedAscendingAssertion<T> : Assertion<IEnumerable<T>, IEnumerable<T>>
	{
		public OrderedAscendingAssertion()
			: base(
				Properties.Resources.PredicateBeOrderedAscending, null,
				a => a != null && (from elem in a orderby elem select elem).SequenceEqual(a), new OrderedMessageBuilder<T>()) {}

		protected override MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> GetMessageBuilderInfo(IEnumerable<T> actual,
		                                                                                            string customMessage)
		{
			MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> mi = base.GetMessageBuilderInfo(actual, customMessage);
			mi.Expected = from elem in mi.Actual orderby elem select elem;
			return mi;
		}
	}
}