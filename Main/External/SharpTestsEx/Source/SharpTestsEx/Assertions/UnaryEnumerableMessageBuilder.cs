using System;
using System.Collections.Generic;

namespace SharpTestsEx.Assertions
{
	public class UnaryEnumerableMessageBuilder<T> : IMessageBuilder<IEnumerable<T>, IEnumerable<T>>
	{
		private readonly Func<IEnumerable<T>, IEnumerable<T>> differencesDelegate;
		public UnaryEnumerableMessageBuilder() {}

		public UnaryEnumerableMessageBuilder(Func<IEnumerable<T>, IEnumerable<T>> differencesDelegate)
		{
			this.differencesDelegate = differencesDelegate;
		}

		public string Compose(MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> info)
		{
			string baseMessage = GetBaseMessage(info);

			var m = new UnaryEnumerableFailureMagnifier<T>(info.Actual, differencesDelegate);
			string magnMessage = m.Message();
			return string.IsNullOrEmpty(magnMessage) ? baseMessage : string.Concat(baseMessage, Environment.NewLine, magnMessage);
		}

		protected string GetBaseMessage(MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> info)
		{
			return string.Format("{0} {1} {2}.{3}", Messages.FormatEnumerable(info.Actual), Properties.Resources.AssertionVerb,
			                     info.AssertionPredicate, info.CustomMessage);
		}
	}
}