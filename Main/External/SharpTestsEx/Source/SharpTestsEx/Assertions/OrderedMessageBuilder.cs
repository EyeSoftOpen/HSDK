namespace SharpTestsEx.Assertions
{
	public class OrderedMessageBuilder<T> : SameSequenceAsMessageBuilder<T> 
	{
		protected override string GetBaseMessage(MessageBuilderInfo<System.Collections.Generic.IEnumerable<T>, System.Collections.Generic.IEnumerable<T>> info)
		{
			return string.Format("{0} {1} {2}.{3}", Messages.FormatEnumerable(info.Actual), Properties.Resources.AssertionVerb,
													 info.AssertionPredicate, info.CustomMessage);
		}
	}
}