using System.Collections.Generic;
using System.Linq;

namespace SharpTestsEx.Assertions
{
	public class OrderedDescendingAssertion<T> : Assertion<IEnumerable<T>, IEnumerable<T>>
	{
		public OrderedDescendingAssertion()
			: base(
				Properties.Resources.PredicateBeOrderedAscending, null,
				a => a != null && (from elem in a orderby elem descending select elem).SequenceEqual(a),
				new OrderedMessageBuilder<T>()) {}

		protected override MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> GetMessageBuilderInfo(IEnumerable<T> actual,
		                                                                                            string customMessage)
		{
			MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> mi = base.GetMessageBuilderInfo(actual, customMessage);
			mi.Expected = from elem in mi.Actual orderby elem descending select elem;
			return mi;
		}
	}
}