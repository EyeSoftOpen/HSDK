using System;
using System.Collections.Generic;

namespace SharpTestsEx.Assertions
{
	public class EnumerableMessageBuilder<T> : IMessageBuilder<IEnumerable<T>, IEnumerable<T>>
	{
		private readonly Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> differencesDelegate;
		public EnumerableMessageBuilder() {}

		public EnumerableMessageBuilder(Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> differencesDelegate)
		{
			this.differencesDelegate = differencesDelegate;
		}

		public Func<IEnumerable<T>, IEnumerable<T>, IEnumerable<T>> DifferencesDelegate
		{
			get { return differencesDelegate; }
		}

		#region Implementation of IMessageBuilder

		public virtual string Compose(MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> info)
		{
			string baseMessage = GetBaseMessage(info);

			var m = new EnumerableFailureMagnifier<T>(info.Actual, info.Expected, differencesDelegate);
			string magnMessage = m.Message();
			return string.IsNullOrEmpty(magnMessage) ? baseMessage : string.Concat(baseMessage, Environment.NewLine, magnMessage);
		}

		#endregion

		protected virtual string GetBaseMessage(MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> info)
		{
			return string.Format("{0} {1} {2} {3}.{4}", Messages.FormatEnumerable(info.Actual), Properties.Resources.AssertionVerb,
													 info.AssertionPredicate, Messages.FormatEnumerable(info.Expected), info.CustomMessage);
		}
	}
}