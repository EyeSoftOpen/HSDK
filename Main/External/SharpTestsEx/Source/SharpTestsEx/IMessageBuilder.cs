namespace SharpTestsEx
{
	public interface IMessageBuilder<TA, TE>
	{
		string Compose(MessageBuilderInfo<TA, TE> info);
	}
}