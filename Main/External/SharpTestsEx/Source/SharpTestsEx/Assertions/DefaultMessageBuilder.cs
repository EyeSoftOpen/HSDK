namespace SharpTestsEx.Assertions
{
	public class DefaultMessageBuilder<TA, TE> : IMessageBuilder<TA, TE>
	{
		public virtual string Compose(MessageBuilderInfo<TA, TE> info)
		{
			return string.Format("{0} {1} {2} {3}.{4}", Messages.FormatValue(info.Actual), Properties.Resources.AssertionVerb, info.AssertionPredicate,
													 Messages.FormatValue(info.Expected), info.CustomMessage);
		}
	}
}