using System;
using System.Collections.Generic;

namespace SharpTestsEx.Assertions
{
	public class SameSequenceAsMessageBuilder<T> : EnumerableMessageBuilder<T>
	{
		public override string Compose(MessageBuilderInfo<IEnumerable<T>, IEnumerable<T>> info)
		{
			string baseMessage = base.Compose(info);

			var m = new SameSequenceAsFailureMagnifier<T>(info.Actual, info.Expected);
			string magnMessage = m.Message();
			return string.IsNullOrEmpty(magnMessage) ? baseMessage : string.Concat(baseMessage, Environment.NewLine, magnMessage);
		}
	}
}